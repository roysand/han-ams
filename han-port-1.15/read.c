
/******************************************************************************
 * Main: read from han-port or file
 *****************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <math.h>
#include <inttypes.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

#ifdef _WIN32
#include <Windows.h>
#else
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h> 
#endif

#include "rs232.h"
#include "aes.h"

typedef struct {
  int act_pow_pos; /* OBIS Code 1.0.1.7.0.255 - Active Power + (Q1+Q4) */
} Items1;

typedef struct {
  char obis_list_version[1000]; /* OBIS Code 1.1.0.2.129.255 - OBIS List Version Identifier */
  char gs1[1000]; /* OBIS Code 0.0.96.1.0.255 - Meter-ID(GIAI GS1 - 16 digits */
  char meter_model[1000]; /* OBIS Code 0.0.96.1.7 - Meter Type */
  int act_pow_pos; /* Active Power + */
  int act_pow_neg; /* Actve Power - */
  int react_pow_pos; /* Reactive Power + */
  int react_pow_neg; /* Reactive Power - */
  int curr_L1; /* Current Phase L1 */
  int volt_L1; /* Voltage L1 */
} Items9;

typedef struct {
  char obis_list_version[1000]; /* OBIS Code 1.1.0.2.129.255 - OBIS List Version Identifier */
  char gs1[1000]; /* OBIS Code 0.0.96.1.0.255 - Meter-ID(GIAI GS1 - 16 digits */
  char meter_model[1000]; /* OBIS Code 0.0.96.1.7.255 - Meter Type */
  int act_pow_pos;  /* Active Power + */
  int act_pow_neg;  /* Active Power - */
  int react_pow_pos;  /* Reactive Power + */
  int react_pow_neg;  /* Reactive Power - */
  int curr_L1;  /* Current Phase L1 mA */
  int curr_L2;  /* Current Phase L2 mA */
  int curr_L3; /* Current Phase L3 mA */
  int volt_L1; /* Voltage L1 */
  int volt_L2; /* Voltage L2 */
  int volt_L3; /* Voltage L3 */
} Items13;

typedef struct {
  char obis_list_version[1000]; /* OBIS Code 1.1.0.2.129.255 - OBIS List Version Identifier */
  char gs1[1000]; /* OBIS Code 0.0.96.1.0.255 - Meter-ID(GIAI GS1 - 16 digits */
  char meter_model[1000]; /* OBIS Code 0.0.96.1.7.255 - Meter Type */
  int act_pow_pos; /* Active Power + */
  int act_pow_neg; /* Active Power - */
  int react_pow_pos; /* Reactive Power + */
  int react_pow_neg; /* Reactive Power - */
  int curr_L1;
  int volt_L1;
  char date_time[1000]; /* OBIS Code: 0.0.1.0.0.255 - Clock and Date in Meter */
  int act_energy_pos;
  int act_energy_neg;
  int react_energy_pos;
  int react_energy_neg;  
} Items14;

typedef struct {
  char obis_list_version[1000]; /* OBIS Code 1.1.0.2.129.255 - OBIS List Version Identifier */
  char gs1[1000]; /* OBIS Code 0.0.96.1.0.255 - Meter-ID(GIAI GS1 - 16 digits */
  char meter_model[1000]; /* OBIS Code 0.0.96.1.7.255 - Meter Type */
  int act_pow_pos; /* Active Power + */
  int act_pow_neg; /* Active Power - */
  int react_pow_pos; /* Reactive Power + */
  int react_pow_neg; /* Reactive Power - */
  int curr_L1; /* Current L1 */
  int curr_L2; /* Current L2 */
  int curr_L3; /* Current L3 */
  int volt_L1; /* Voltage L1 */
  int volt_L2; /* Voltage L2 */
  int volt_L3; /* Voltage L3 */
  char date_time[1000]; /* OBIS Code: 0.0.1.0.0.255 - Clock and Date in Meter */
  int act_energy_pa; /* Active Energy +A */
  int act_energy_ma; /* Active Energy -A */
  int act_energy_pr; /* Active Energy +R */
  int act_energy_mr; /* Active Energy -R */
} Items18;

/*
7e                                                     : Flag (0x7e)
a0 87                                                  : Frame Format Field
01 02                                                  : Source Address
01                                                     : Destination Address
10                                                     : Control Field = R R R P/F S S S 0 (I Frame)
9e 6d                                                  : HCS
e6 e7 00                                               : DLMS/COSEM LLC Addresses
0f 40 00 00 00                                         : DLMS HEADER?
09 0c 07 d0 01 03 01 0e 00 0c ff 80 00 03              : Information
02 0e                                                  : Information
09 07 4b 46 4d 5f 30 30 31                             : Information
09 10 36 39 37 30 36 33 31 34 30 30 30 30 30 39 35 30  : Information
09 08 4d 41 31 30 35 48 32 45                          : Information
06 00 00 00 00                                         : Information
06 00 00 00 00                                         : Information
06 00 00 00 00                                         : Information
06 00 00 00 00                                         : Information
06 00 00 00 0e                                         : Information
06 00 00 09 01                                         : Information
09 0c 07 d0 01 03 01 0e 00 0c ff 80 00 03              : Information
06 00 00 00 00                                         : Information
06 00 00 00 00                                         : Information
06 00 00 00 00                                         : Information
06 00 00 00 00                                         : Information
97 35                                                  : FCS
7e                                                     : Flag
*/

typedef struct {
  int year;
  int month;
  int day;
  int hour;
  int min;
  int sec;
  char tm[100];
  int num_items;
  union {
    Items1 msg1;   /* List 1: 1-and-3-phase */
    Items9 msg9;   /* List 2: 1-phase */
    Items13 msg13; /* List 2: 3-phase */
    Items14 msg14; /* List 3: 1-phase */ 
    Items18 msg18; /* List 3: 3-phase */
  };
} HanMsg;

HanMsg msg;

/**********************************************************
 * FUNCTION: verifyMessage()
 *********************************************************/
int verifyMessage(unsigned char *buf,
		  unsigned int buf_len)
{
  /* warning killers */
  buf = buf;
  buf_len = buf_len;
  
  return 0;
}

/**********************************************************
 * FUNCTION: decodeMessage()
 *********************************************************/
int decodeMessage(unsigned char *buf,
		  int buf_len,
		  HanMsg *msg)
{
  /* warning killers */
  buf_len = buf_len;
  
  memset(msg, 0, sizeof(HanMsg)); /* clear/init data */
  
  if (buf[17] == 0x09) {

    msg->year = buf[19]<<8 | buf[20];
    msg->month = buf[21];
    msg->day = buf[22];
    msg->hour = buf[24];
    msg->min = buf[25];
    msg->sec = buf[26];
    
    sprintf(msg->tm, "%d-%02d-%02d %02d:%02d:%02d", msg->year, msg->month, msg->day, msg->hour, msg->min, msg->sec);

    int offset = 17 + 2 + buf[18];
    if (buf[offset]== 0x02) {
      msg->num_items = buf[offset+1];
    }
    offset+=2;
    if (msg->num_items == 1) { /* Num Records: 1 */
      if (buf[offset]==0x06) { /* Item 1 */
	msg->msg1.act_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
    }
    else if (msg->num_items==9) { /* Num records: 9 */
      unsigned int num_bytes=0;
      if (buf[offset]==0x09) { /* Item 1 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg9.obis_list_version, (const char *) buf+offset+2, num_bytes);
	msg->msg9.obis_list_version[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 2 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg9.gs1, (const char *) buf+offset+2, num_bytes);
	msg->msg9.gs1[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 3 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg9.meter_model, (const char *) buf+offset+2, num_bytes);
	msg->msg9.meter_model[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x06) { /* Item 4 */
	msg->msg9.act_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 5 */
	msg->msg9.act_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 6 */
	msg->msg9.react_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 7 */
	msg->msg9.react_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 8 */
	msg->msg9.curr_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 9 */
	msg->msg9.volt_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
    }
    else if (msg->num_items==13) { /* Num records: 13 */
      unsigned int num_bytes = 0;
      if (buf[offset]==0x09) { /* Item 1 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg13.obis_list_version, (const char *) buf+offset+2, num_bytes);
	msg->msg13.obis_list_version[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 2 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg13.gs1, (const char *) buf+offset+2, num_bytes);
	msg->msg13.gs1[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 3 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg13.meter_model, (const char *) buf+offset+2, num_bytes);
	msg->msg13.meter_model[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x06) { /* Item 4 */
	msg->msg13.act_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 5 */
	msg->msg13.act_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 6 */
	msg->msg13.react_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 7 */
	msg->msg13.react_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 8 */
	msg->msg13.curr_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 9 */
	msg->msg13.curr_L2 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 10 */
	msg->msg13.curr_L3 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 11 */
	msg->msg13.volt_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 12 */
	msg->msg13.volt_L2 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 13 */
	msg->msg13.volt_L3 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
    }
    else if (msg->num_items==14) { /* Num records: 14 */
      unsigned int num_bytes = 0;
      if (buf[offset]==0x09) { /* Item 1 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg14.obis_list_version, (const char *) buf+offset+2, num_bytes);
	msg->msg14.obis_list_version[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 2 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg14.gs1, (const char *) buf+offset+2, num_bytes);
	msg->msg14.gs1[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 3 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg14.meter_model, (const char *) buf+offset+2, num_bytes);
	msg->msg14.meter_model[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x06) { /* Item 4 */
	msg->msg14.act_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 5 */
	msg->msg14.act_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 6 */
	msg->msg14.react_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 7 */
	msg->msg14.react_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 8 */
	msg->msg14.curr_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 9 */
	msg->msg14.volt_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x09) { /* Item 10 */
	num_bytes = buf[offset+1];

	int year = buf[offset+2]<<8 | buf[offset+3];
	int month = buf[offset+4];
	int day = buf[offset+5];
	int hour = buf[offset+7];
	int min = buf[offset+8];
	int sec = buf[offset+9];
    
	sprintf(msg->msg14.date_time, "%d-%02d-%02d %02d:%02d:%02d", year, month, day, hour, min, sec);

      }
      offset+=2+num_bytes;
      if (buf[offset]==0x06) { /* Item 11 */
	msg->msg14.act_energy_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 12 */
	msg->msg14.act_energy_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 13 */
	msg->msg14.react_energy_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 14 */
	msg->msg14.react_energy_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
    }
    else if (msg->num_items==18) { /* Num records: 18 */
      unsigned int num_bytes = 0;
      if (buf[offset]==0x09) { /* Item 1 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg18.obis_list_version, (const char *) buf+offset+2, num_bytes);
	msg->msg18.obis_list_version[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 2 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg18.gs1, (const char *) buf+offset+2, num_bytes);
	msg->msg18.gs1[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x09) { /* Item 3 */
	num_bytes = buf[offset+1];
	strncpy(msg->msg18.meter_model, (const char *) buf+offset+2, num_bytes);
	msg->msg18.meter_model[num_bytes] = '\0';
      }
      offset+=2+num_bytes;
      if (buf[offset]==0x06) { /* Item 4 */
	msg->msg18.act_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 5 */
	msg->msg18.act_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 6 */
	msg->msg18.react_pow_pos = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 7 */
	msg->msg18.react_pow_neg = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 8 */
	msg->msg18.curr_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 9 */
	msg->msg18.curr_L2 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 10 */
	msg->msg18.curr_L3 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 11 */
	msg->msg18.volt_L1 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 12 */
	msg->msg18.volt_L2 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 13 */
	msg->msg18.volt_L3 = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x09) { /* Item 14 */
	num_bytes = buf[offset+1];

	int year = buf[offset+2]<<8 | buf[offset+3];
	int month = buf[offset+4];
	int day = buf[offset+5];
	int hour = buf[offset+7];
	int min = buf[offset+8];
	int sec = buf[offset+9];
    
	sprintf(msg->msg18.date_time, "%d-%02d-%02d %02d:%02d:%02d", year, month, day, hour, min, sec);

      }
      offset+=2+num_bytes;
      if (buf[offset]==0x06) { /* Item 15 */
	msg->msg18.act_energy_pa = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 16 */
	msg->msg18.act_energy_ma = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 17 */
	msg->msg18.act_energy_pr = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
      offset+=1+4;
      if (buf[offset]==0x06) { /* Item 18 */
	msg->msg18.act_energy_mr = buf[offset+1]<<24 | buf[offset+2]<<16 | buf[offset+3]<<8 | buf[offset+4];
      }      
    }
    else {
      return 1; /* Error - Unknown msg size */
    }
    return 0; /* Message OK */
  }
  
  return 1; /* Error - Unknown msg type */
}

/**********************************************************
 * FUNCTION: printMessage()
 *********************************************************/
int printMessage(unsigned char *buf,
		 int buf_len,
		 HanMsg *msg,
		 char *tm_str,
		 FILE *rec_file)
{
  int i;
  int debug_hex = 0;
  
  for(i=0; i<buf_len; i++) {
    if (i>0) {
      if (debug_hex) printf(" ");
    }
    if (debug_hex) printf("%02x", buf[i]);
    if (rec_file) fputc(buf[i], rec_file);
  }
  
  if (rec_file) fflush(rec_file);

  printf("{\"Date_Time\":\"%s\",\n", msg->tm);
  if (tm_str) {
    int year, month, day;
    int hour, min, sec;
    
    /* FORMAT = "2017-06-07 22:25:30" */
    if (sscanf(msg->tm, "%4d-%2d-%2d %2d:%2d:%2d",
	       &year, &month, &day,
	       &hour, &min, &sec) == 6) {

      struct tm info;
      time_t tm_sec;
      
      info.tm_year = year - 1900;
      info.tm_mon = month - 1;
      info.tm_mday = day;
      info.tm_hour = hour;
      info.tm_min = min;
      info.tm_sec = sec;
      info.tm_isdst = -1;
      
      tm_sec = mktime(&info);
      
      printf("\"Meter_Time\":%1ld,\n", tm_sec);
    }
    printf("\"Host_Time\":%s,\n", tm_str);
  }

  switch (msg->num_items) {
  case 1:
    printf("\"Act_Pow_P_Q1_Q4\":%1d}\n", msg->msg1.act_pow_pos);
    break;
  case 9:
    printf("\"OBIS_List_Version\":\"%s\",\n", msg->msg9.obis_list_version);
    printf("\"GS1\":\"%s\",\n", msg->msg9.gs1);
    printf("\"Meter_Model\":\"%s\",\n", msg->msg9.meter_model);
    printf("\"Act_Pow_P_Q1_Q4\":%1d,\n", msg->msg9.act_pow_pos);
    printf("\"Act_Pow_M_Q2_Q3\":%1d,\n", msg->msg9.act_pow_neg);
    printf("\"React_Pow_P\":%1d,\n", msg->msg9.react_pow_pos);
    printf("\"React_Pow_M\":%1d,\n", msg->msg9.react_pow_neg);
    printf("\"Curr_L1\":%1d,\n", msg->msg9.curr_L1);
    printf("\"Volt_L1\":%1d}\n", msg->msg9.volt_L1);
    break;
  case 13:
    printf("\"OBIS_List_Version\":\"%s\",\n", msg->msg13.obis_list_version);
    printf("\"GS1\":\"%s\",\n", msg->msg13.gs1);
    printf("\"Meter_Model\":\"%s\",\n", msg->msg13.meter_model);
    printf("\"Act_Pow_P_Q1_Q4\":%1d,\n", msg->msg13.act_pow_pos);
    printf("\"Act_Pow_M_Q2_Q3\":%1d,\n", msg->msg13.act_pow_neg);
    printf("\"React_Pow_P_Q1_Q2\":%1d,\n", msg->msg13.react_pow_pos);
    printf("\"React_Pow_M_Q3_Q4\":%1d,\n", msg->msg13.react_pow_neg);
    printf("\"Curr_L1\":%1d,\n", msg->msg13.curr_L1);
    printf("\"Curr_L2\":%1d,\n", msg->msg13.curr_L2);
    printf("\"Curr_L3\":%1d,\n", msg->msg13.curr_L3);
    printf("\"Volt_L1\":%1d,\n", msg->msg13.volt_L1);
    printf("\"Volt_L2\":%1d,\n", msg->msg13.volt_L2);
    printf("\"Volt_L3\":%1d}\n", msg->msg13.volt_L3);
    break;
  case 14:
    printf("\"OBIS_List_Version\":\"%s\",\n", msg->msg14.obis_list_version);
    printf("\"GS1\":\"%s\",\n", msg->msg14.gs1);
    printf("\"Meter_Model\":\"%s\",\n", msg->msg14.meter_model);
    printf("\"Act_Pow_P_Q1_Q4\":%1d,\n", msg->msg14.act_pow_pos);
    printf("\"Act_Pow_M_Q2_Q3\":%1d,\n", msg->msg14.act_pow_neg);
    printf("\"React_Pow_P_Q1_Q2\":%1d,\n", msg->msg14.react_pow_pos);
    printf("\"React_Pow_M_Q3_Q4\":%1d,\n", msg->msg14.react_pow_neg);
    printf("\"Curr_L1\":%1d,\n", msg->msg14.curr_L1);
    printf("\"Volt_L1\":%1d,\n", msg->msg14.volt_L1);
    printf("\"Date_Time2\":\"%s\",\n", msg->msg14.date_time);
    printf("\"Act_Energy_P\":%1d,\n", msg->msg14.act_energy_pos);
    printf("\"Act_Energy_M\":%1d,\n", msg->msg14.act_energy_neg);
    printf("\"React_Energy_P\":%1d,\n", msg->msg14.react_energy_pos);
    printf("\"React_Energy_M\":%1d}\n", msg->msg14.react_energy_neg);
    break;
  case 18:
    printf("\"OBIS_List_Version\":\"%s\",\n", msg->msg18.obis_list_version);
    printf("\"GS1\":\"%s\",\n", msg->msg18.gs1);
    printf("\"Meter_Model\":\"%s\",\n", msg->msg18.meter_model);
    printf("\"Act_Pow_P_Q1_Q4\":%1d,\n", msg->msg18.act_pow_pos);
    printf("\"Act_Pow_M_Q2_Q3\":%1d,\n", msg->msg18.act_pow_neg);
    printf("\"React_Pow_P_Q1_Q2\":%1d,\n", msg->msg18.react_pow_pos);
    printf("\"React_Pow_M_Q3_Q4\":%1d,\n", msg->msg18.react_pow_neg);
    printf("\"Curr_L1\":%1d,\n", msg->msg18.curr_L1);
    printf("\"Curr_L2\":%1d,\n", msg->msg18.curr_L2);
    printf("\"Curr_L3\":%1d,\n", msg->msg18.curr_L3);
    printf("\"Volt_L1\":%1d,\n", msg->msg18.volt_L1);
    printf("\"Volt_L2\":%1d,\n", msg->msg18.volt_L2);
    printf("\"Volt_L3\":%1d,\n", msg->msg18.volt_L3);
    printf("\"Date_Time2\":\"%s\",\n", msg->msg18.date_time);
    printf("\"Act_Energy_P\":%1d,\n", msg->msg18.act_energy_pa);
    printf("\"Act_Energy_M\":%1d,\n", msg->msg18.act_energy_ma);
    printf("\"React_Energy_P\":%1d,\n", msg->msg18.act_energy_pr);
    printf("\"React_Energy_M\":%1d}\n", msg->msg18.act_energy_mr);
    break;
  }

  printf("\n");

  return 0;
}

/**********************************************************
 * FUNCTION: my_read()
 *********************************************************/
int my_read(int fd,
	    unsigned char *buf,
	    int len)
{
  int n;
  
  while (1) {
    n = read(fd, buf, len);

    if (n>=0) {
      break;
    }
    if (errno == EAGAIN) {
      usleep(1000);
    }
  }
  
  return n;
}

/**********************************************************
 * FUNCTION: readMessage()
 *********************************************************/
int readMessage(int read_fd,
		unsigned char *buf,
		FILE *rec_f,
		int print_hex_msg)
{
  int n;
  int debug;
  int length;

  debug=0;

  if (rec_f) {
    while(0) { /* dump data and exit */
      static int count=0;
      /* next byte */
      n = my_read(read_fd, buf, 4096);
      if (n>0) {
	fprintf(stderr, "*");
	fwrite(buf, n, 1, rec_f);
	fflush(rec_f);
      }
      count+=n;
      if (count>500) {
	fclose(rec_f);
	exit(0);
      }
    }
  }
    
  debug=0;
  while(1) { /* find start of header */
    /* next byte */
    n = my_read(read_fd, buf, 1);
    if (n==-1) {
      printf("read()==-1: errno: %s\n", strerror(errno));
    }
    if (n<=0) return 0; /* EOF or ERROR */

    if (debug) { printf(" A(%02x)", buf[0]); fflush(stdout);}

    if (buf[0] == 0x7e) { /* good, frame start delimiter candidate */
      /* next byte */
      n = my_read(read_fd, buf+1, 1);
      if (debug) {printf(" B(%02x)", buf[1]); fflush(stdout);};
      if (n<=0) return 0; /* EOF or ERROR */

      
      /* 2 Byte Frame Format Field
	 16 BITS: "TTTTSLLLLLLLLLLL"
	 - T=Type bits: TTTT = 0101 (0xa0) = Type 3
	 - S=Segmentation=0 (Segment = 1)
	 - L=11 Length Bits
      */
      if ((buf[1] & 0xa0) == 0xa0) { /* 16 bit frame format field = "0101SLLLLLLLLLLL" - Type 3 = 0101 (0xa0), S=Segmentation, L=Length bits */
	n = my_read(read_fd, buf+2, 1);
	if (debug) {printf(" C(%02x)", buf[2]); fflush(stdout);}
	if (n<=0) return 0; /* EOF or ERROR */
	length = ((buf[1] & 0x07) << 8) + buf[2]; /* length is 11 bits */
	break;
      }
    }
  }

  /* read rest of message */
  int bytes_read = 3;
  int frame_delimiters = 2; /* start byte 0x7e + end byte 0x7e*/
  int bytes_left = length + frame_delimiters - bytes_read;
  
  while (bytes_left > 0) {
    n = my_read(read_fd, buf+bytes_read, bytes_left);
    if (debug) { printf(" M()"); fflush(stdout);}
    if (n<=0) return 0; /* EOF or ERROR */

    bytes_read += n;
    bytes_left -= n;
  }

  if (print_hex_msg) {
    int i;
    for (i=0; i<bytes_read; i++) {
      printf("%02x ", buf[i]);
    }
    printf("\n");
  }
  
  return(bytes_read);
}

/************************************************************************
 * FUNCTION: decryptMessage()
 ***********************************************************************/
int decryptMessage(uint8_t *msg,
		   int msg_len,
		   uint8_t *key)
{
  int debug=0;
  int first = 12;
  int last = msg_len - 3;

  int offset=first;

  uint8_t decrypted[16]; 
  
  while (offset<last)  {
    if (debug) printf("### remains: %1d\n", last-offset);
    AES128_ECB_decrypt(msg+offset, key, decrypted);
    memcpy(msg+offset, decrypted, 16);
    offset += 16;
  }
  
  return 0;
}

/**********************************************************************
 * FUNCTION: send_message()
 **********************************************************************/
int send_message(int l_fd_mcast,
		 struct sockaddr_in *l_addr,
		 unsigned char *l_msg,
		 int l_msg_len)
{
  if (sendto(l_fd_mcast, l_msg, l_msg_len, 0, (const struct sockaddr *) l_addr, sizeof(*l_addr)) < 0) {
       perror("sendto");
       return -1;
  }
  
  return 0;
}

/**********************************************************************
 * FUNCTION: open_mc_send_socket()
 **********************************************************************/
int open_mc_send_socket(struct sockaddr_in *addr,
			char *mcast_addr,
			int mcast_port)
{
  int debug=0;
  int l_fd;
  
  /* create what looks like an ordinary UDP socket */
  if ((l_fd=socket(AF_INET,SOCK_DGRAM,0)) < 0) {
    perror("socket");
    return -1;
  }
  
  /* set up destination address */
  memset(addr, 0, sizeof(*addr));
  addr->sin_family = AF_INET;
  addr->sin_addr.s_addr = inet_addr(mcast_addr);
  addr->sin_port = htons(mcast_port);

  if (debug) printf("mc send socket: %1d\n", l_fd);
  
  return l_fd;
}

/**********************************************************************
 * FUNCTION: open_mc_listen_socket()
 **********************************************************************/
int open_mc_listen_socket(struct sockaddr_in *l_mc_addr,
			  char *l_mc_host,
			  int l_mc_port)
{
  int debug=0;
  int l_fd;
  unsigned int yes=1;
  
  /* create what looks like an ordinary UDP socket */
  if ((l_fd = socket(AF_INET, SOCK_DGRAM, 0)) < 0) {
    perror("socket");
    return -1;
  }

  if (setsockopt(l_fd, SOL_SOCKET, SO_REUSEADDR, &yes, sizeof(yes)) < 0) {
    perror("Reusing ADDR failed");
    exit(1);
  }
  
  if (debug) printf("mc listen socket: %1d\n", l_fd);

  /* set up destination address */
  memset(l_mc_addr, 0, sizeof(*l_mc_addr));
  l_mc_addr->sin_family = AF_INET;
  l_mc_addr->sin_addr.s_addr = htonl(INADDR_ANY);
  l_mc_addr->sin_port = htons(l_mc_port);

  if (bind(l_fd, (const struct sockaddr *) l_mc_addr, sizeof(*l_mc_addr)) == -1) {        
      perror("bind");
      exit(1);
  }

  struct ip_mreq l_mreq;
  
  /* receive */
  l_mreq.imr_multiaddr.s_addr = inet_addr(l_mc_host);         
  l_mreq.imr_interface.s_addr = htonl(INADDR_ANY);         
  if (setsockopt(l_fd, IPPROTO_IP, IP_ADD_MEMBERSHIP, &l_mreq, sizeof(l_mreq)) < 0) {
    perror("setsockopt mreq");
    exit(1);
  }           
  
  if (debug) printf("mc listen socket: %1d\n", l_fd);
  return l_fd;
}

/**********************************************************************
 * FUNCTION: open_usocket()
 *********************************************************************/
int open_usocket(char *hostname,
		 int port_num)
{
  /* tcp socket */  
  int sockfd = socket(AF_INET, SOCK_STREAM, 0);
  
  if (sockfd < 0) { 
    fprintf(stderr, "ERROR opening socket");
    return -1;
  }

  /* server/ip address */
  struct hostent *server = gethostbyname(hostname);

  if (server == NULL) {
    printf("ERROR, no such host");
    return -1;
  }

  struct sockaddr_in serv_addr;
  
  memset((char *)&serv_addr, 0, sizeof(serv_addr));
  serv_addr.sin_family = AF_INET;
  memcpy((char *)&serv_addr.sin_addr.s_addr,
	 (char *)server->h_addr, 
	 server->h_length);
  serv_addr.sin_port = htons(port_num);
  
  if (connect(sockfd, (struct sockaddr *)&serv_addr, sizeof(serv_addr)) < 0) {
    printf("connect() error\n");
    return -1;
  }  
  return sockfd;
}  

/************************************************************************
 * FUNCTION: usage()
 ***********************************************************************/
void usage(char *pname,
	   int trap)
{
  printf("usage(trap) ==  %1d\n", trap);
  printf("usage: %s [-i|x|n|m|M|l] [-f <filename>] [-k <decrypt-key>] [-h <hostname|ip-address> [-p <tcp port>]] [-d <device] [-P <E|O|N>\n", pname);
  printf("             -i : Print intial settings\n");
  printf("             -x : Print message in hex\n");
  printf("             -n : No logging to file\n");
  printf("             -f : Replay from file\n");
  printf("             -h : Read MBUS via TCP adapter, host/ip\n");
  printf("             -p : Read MBUS via TCP adapter, port num\n");
  printf("             -k : AES128 key in hex, 32 characters á 4 bit\n");
  printf("             -d : Read from serial device\n");
  printf("             -P : serial port byte parity N(one) E(ven) O(dd)\n");
  printf("             -m : Tx Multicast HAN messages\n");
  printf("             -M : Tx Multicast HAN messages, sleep 1s after send (when replay from file)\n");
  printf("             -l : Rx Multicast HAN messages\n");

  exit (1);
}

/************************************************************************
 * FUNCTION: main()
 ***********************************************************************/
int main(int argc,
	 char *argv[])
{
  int debug=0;
  int print_init = 0;
  int print_hex = 0;
#ifndef SERIAL_PORT 
  char serial_device[100] = "/dev/ttyS0";
#else
  char serial_device[100] = SERIAL_PORT;
#endif

  char mode[] = {'8', /* bits in byte */
		 'E', /* parity - E(ven), O(dd) or N(one) */
		 '1', /* num stop bits */
		 0}; /* end of string */
  int bdrate = 2400; /* 2400 baud */

  int read_serial = 1;
  int read_tcp_socket = 0;
  int read_mcast_socket = 0;
  int read_file = 0;
  int write_file = 1;
  int send_mcast = 0;
  int sleep_1s = 0;
  char mcast_ip_addr[100] = "239.255.199.34";
  int mcast_ip_port = 23932;
  int fd_mcast;
  struct sockaddr_in mcast_addr;
  char host[30] = "<undefined>";
  char fname[200] = "empty";
  uint8_t key[16] = {0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
		     0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff};
  int port_num=10001;
  int decrypt=0;

  char *pname = argv[0];
  char *match = pname;
  while ((match = strstr(match, "/")) != NULL) {
      match++;
      pname = match;
  }

  if (debug) printf("num args: %d\n", argc);

  int a;
  
  for (a=1; a<argc; a++) {
    if (strcmp(argv[a], "-f")==0) {
      if (a+1==argc) {
	usage(pname, 0);
      }
      a++;
      strcpy(fname, argv[a]);
      read_serial = 0;
      read_tcp_socket = 0;
      read_file = 1;
      write_file = 0;
    }
    else if (strcmp(argv[a], "-k")==0) {
      if (a+1==argc) {
	usage(pname, 1);
      }
      a++;
      if (strlen(argv[a])!=32) {
	usage(pname, 99);
      }
      int k,l;
      for (k=0, l=0; k<16; k++, l+=2) {
	char str[10];
	sprintf(str, "%c%c%c", argv[a][l], argv[a][l+1], '\0');
	unsigned int val;
	if (sscanf(str, "%02x", &val)==0) {
	  usage(pname, 98);
	}
	key[k] = val;
	if (debug) printf("str: %1s, key[%1d] = %1d\n", str, k, key[k]);
      }
      decrypt=1;
    }
    else if (strcmp(argv[a], "-h")==0) {
      if (a+1==argc) {
	usage(pname, 3);
      }
      a++;
      strcpy(host, argv[a]);
      read_serial = 0;
      read_tcp_socket = 1;
      read_file = 0;
    }
    else if (strcmp(argv[a], "-p")==0) {
      if (a+1==argc) {
	usage(pname, 4);
      }
      a++;
      port_num = atoi(argv[a]);
      read_serial = 0;
      read_tcp_socket = 1;
      read_file = 0;
    }
    else if (strcmp(argv[a], "-l")==0) {
      strcpy(host, mcast_ip_addr);
      read_serial = 0;
      read_tcp_socket = 0;
      read_mcast_socket = 1;
      read_file = 0;
    }
    else if (strcmp(argv[a], "-m")==0) {
      send_mcast = 1;
      sleep_1s = 0;
    }
    else if (strcmp(argv[a], "-M")==0) {
      send_mcast = 1;
      sleep_1s = 1;
    }
    else if (strcmp(argv[a], "-d")==0) {
      if (a+1==argc) {
	usage(pname, 4);
      }
      a++;
      strcpy(serial_device, argv[a]);
    }
    else if (strcmp(argv[a], "-P")==0) {
      if (a+1==argc) {
	usage(pname, 4);
      }
      a++;
      switch (argv[a][0]) {
      case 'E':
	mode[1] = argv[a][0];
	break;
      case 'O':
	mode[1] = argv[a][0];
	break;
      case 'N':
	mode[1] = argv[a][0];
	break;
      default:
	usage(pname, 4);
      }
    }
    else if (strcmp(argv[a], "-n")==0) {
      write_file = 0;
    }
    else if (strcmp(argv[a], "-i")==0) {
      print_init = 1;
    }
    else if (strcmp(argv[a], "-x")==0) {
      print_hex = 1;
    }
    else {
      usage(pname, 5);
    }
  }

  if (read_tcp_socket && strcmp(host, "<undefined>")==0) {
    usage(pname, 80);
  }

  if (print_init) {
    printf("\n###################### SETTINGS #######################\n");
    printf("read serial: %1d\n", read_serial);
    printf("  serial_device: %s, bitrate: %1d\n", serial_device, bdrate);
    printf("read mcast socket: %1d\n", read_mcast_socket);
    printf("  mcast ip: %s\n", mcast_ip_addr);
    printf("  mcast port: %1d\n",mcast_ip_port);
    printf("read file: %1d\n", read_file);
    printf("  fname: %s\n", fname);
    printf("write file: %1d\n", write_file);
    printf("read tty tcp socket: %1d\n", read_tcp_socket);
    printf("  tcp host: %s\n", host);
    printf("  tcp port: %1d\n", port_num);
    printf("decrypt messages: %1d\n", decrypt);
    printf("  key: %s\n", key);
  }

  /* OPEN LOG FILE ? */
  FILE *rec_f = NULL;

  if (write_file) { /* read from rs232 and log to file? */
    sprintf(fname, "han-data-%1ld.dat", clock()); 
    if ((rec_f=fopen(fname, "w+"))==NULL) {
      printf("could not open han log file\n");
      return 1;
    }
  }

  int read_fd = 0;

  /* Open FILE, RS232, TCP SOCKET or MCAST (UDP) SOCKET? */
  if (read_file) {
    if ((read_fd = open(fname, O_RDONLY)) == -1) {
      printf("can't open file: %s\n", fname);
      usage(pname, 6);
    }
    else {
      if (debug) printf("open_file OK: %1d\n", read_fd);
    }
  }
  else if (read_serial) {
    if ((read_fd = open_serial(serial_device, bdrate, mode)) == -1) {
      printf("Can not open rs232 port %s\n", serial_device);
      return -1;
    }
    else {
      if (debug) printf("open_serial OK: %1d\n", read_fd);
    }
  }
  else if (read_tcp_socket) {
    if ((read_fd = open_usocket(host, port_num)) == -1) {
      printf("Can not open tcp socket %s:%1d\n", host, port_num);
      return -1;
    }
    else {
      if (debug) printf("open_usocket OK: %1d\n", read_fd);
    }
  }
  else if (read_mcast_socket) {

    if ((read_fd = open_mc_listen_socket(&mcast_addr, mcast_ip_addr, mcast_ip_port)) == -1) {
      printf("Can not open udp mc socket: %1d\n", read_fd);
      exit(1);
    }
    if (debug) printf("open_mc listen socket returned: %1d\n", read_fd);
    
  }

  if (send_mcast) {
    fd_mcast = open_mc_send_socket(&mcast_addr, mcast_ip_addr, mcast_ip_port);
  }

  if (print_init) {
    printf("#######################################################\n\n");
  }
  
  int buf_len;
  unsigned char buf[4096];
  unsigned char buf2[4096];
  socklen_t mcast_addrlen = sizeof(mcast_addr);

  long ms; // Milliseconds
  time_t s;  // Seconds
  struct timespec spec;
  char tmstr64[100];

  while (1) {
    
    if (read_mcast_socket) { /* MULTICAST RECEIVE */
      buf_len = recvfrom(read_fd, buf, sizeof(buf), 0, 
			 (struct sockaddr *) &mcast_addr, &mcast_addrlen);
      if (buf_len < 0) {
	perror("recvfrom");
	exit(1);
      } else if (buf_len == 0) {
	break;
      }
      if (debug) printf("%s: message = \"%s\"\n", inet_ntoa(mcast_addr.sin_addr), buf);
    }
    else { /* SERIAL, FILE or TCP RECEIVE */
      if ((buf_len = readMessage(read_fd, buf, rec_f, print_hex)) <= 0) {
	
	break;
      }
    }
    
    if (verifyMessage(buf, buf_len)) {
      printf("verify failed\n");
      continue;
    }

    /* save received buffer */
    memcpy(buf2, buf, buf_len);
   
    if (decrypt) {
      decryptMessage(buf, buf_len, key);
    }
    
    if (send_mcast) { /* send orig buffer */
      send_message(fd_mcast, &mcast_addr, buf2, buf_len);
      if (sleep_1s) { /* only when replay from file */
	sleep(1);
      }
    }

    char *tm_str = NULL;

    if (1) { /* was: if (read_serial) { */

      clock_gettime(CLOCK_REALTIME, &spec); /* OR: CLOCK_MONOTONIC */
      s  = spec.tv_sec;
      ms = round(spec.tv_nsec / 1.0e6); // Convert nanoseconds to milliseconds
      
      strftime(tmstr64, 20, "%Y-%m-%d %H:%M:%S", localtime(&s));
      char ms_str[10];
      sprintf(ms_str, "%.ld", ms);
      strcat(tmstr64, ms_str);
      sprintf(tmstr64, "%ld.%03ld", s, ms);
      
      tm_str = (char*) &tmstr64;
    }
    
    if (decodeMessage(buf, buf_len, &msg)) {
      if (debug) printf("decode failed\n");
      printMessage(buf, buf_len, &msg, tm_str, rec_f);
      continue;
    }
    printMessage(buf, buf_len, &msg, tm_str, rec_f);
    if (debug) printf("print done\n");
    fflush(stdout);
  }
  
  return 0;
}

/**************************** end of file **************************************/

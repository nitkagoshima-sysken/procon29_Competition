#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

#include "getopt.h"

void makeField(int, int,int,int,char*);
char* makeFileName(char*, int ,int);

int main(int argc,char* argv[]){
  int vertical = 12;
  int side = 12;
  int type = 3;
  int number = 1;
  int opt,i;
  char* filename = "code.txt";

  opterr = 0;

  while((opt = getopt(argc, argv,"v:s:t:n:")) != -1){
    switch(opt){
      case 'v':vertical = atoi(optarg);break;
      case 's':side = atoi(optarg);break;
      case 't':type = atoi(optarg);break;
      case 'n':number = atoi(optarg);break;
      default: printf("Usage: %s [-v argment] [-s argment] [-t argment] [-n argment] filename ...\n", argv[0]);break;
    }
  }

  argc -= optind;
  argv += optind;

  if(argv[0] != NULL)filename = argv[0];
  makeField(vertical, side, type, number, filename);

  return 0;
}

char* makeFileName(char* filename, int number, int size){
  char* result;
  int i,j;

  result = calloc(strlen(filename)+size+1,sizeof(char));

  for(i=0;filename[i]!='.';i++)result[i]=filename[i];
  for(j=size;j>0;j--,number/=10)result[i+j-1]= number%10 + '0';
  for(;filename[i]!='\0';i++)result[i+size] = filename[i];

  return result;
}

void makeField(int vertical, int side, int type, int number, char* filename){
  int data[12][12];
  int i,j,size,k;
  int point;
  char* madefilename;

  FILE *fp;

  srand((unsigned)time(NULL));

  i = number;
  for(size=0;i != 0;size++,i/= 10);
  if(number==1)size = 0;

  for(k=1;k<=number;k++){
    if(type == 1){
      for(i = 0;i<vertical;i++){
        for(j = 0;j<=side/2;j++){
          point = (rand()%33)-16;
          data[i][j] = point;
          data[i][side-1-j] = point;
        }
      }
    }else if(type == 2){
      for(i = 0;i<=vertical/2;i++){
        for(j = 0;j<side;j++){
          point = (rand()%33)-16;
          data[i][j] = point;
          data[vertical-1-i][j] = point;
        }
      }
    }else if(type == 3){
      for(i = 0;i<=vertical/2;i++){
        for(j = 0;j<=side/2;j++){
          point = (rand()%33)-16;
          data[i][j] = point;
          data[i][side-1-j] = point;
          data[vertical-1-i][j] = point;
          data[vertical-1-i][side-1-j] = point;
        }
      }
    }

    madefilename = makeFileName(filename, k, size);
    fp = fopen(madefilename,"w");
    if(fp == NULL){
      fprintf(stderr,"Can't open file:\"%s\"\n");
      exit(1)
    }
    free(madefilename);

    fprintf(fp,"%d %d:",vertical,side);

    for(i = 0;i < vertical;i++){
      for(j = 0;j < side;j++)fprintf(fp,"%d ",data[i][j]);
      fprintf(fp,":");
    }

    fprintf(fp,"%d %d:%d %d:",rand()%vertical-1,rand()%side-1,rand()%vertical-1,rand()%side-1);

    fclose(fp);
  }
}

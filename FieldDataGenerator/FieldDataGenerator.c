#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>
#include <unistd.h>

void makeField(int,int,int,char*);

int main(int argc,char* argv[]){
  int vertical = 12;
  int side = 12;
  int type = 3;
  int opt;
  char* filename = "code.txt";

  opterr = 0;

  while((opt = getopt(argc, argv,"v:s:t:")) != -1){
    switch(opt){
      case 'v':vertical = atoi(optarg);break;
      case 's':side = atoi(optarg);break;
      case 't':type = atoi(optarg);break;
      default: printf("Usage: %s [-v argment] [-s argment] [-y argment] filename ...\n", argv[0]);break;
    }
  }

  argc -= optind;
  argv += optind;

  if(argv[0] != NULL)filename = argv[0];
  makeField(vertical, side, type ,filename);

  return 0;
}

void makeField(int vertical, int side, int type, char* filename){
  int data[12][12];
  int i,j;
  int point;

  FILE *fp;

  srand((unsigned)time(NULL));

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

  fp = fopen(filename,"w");

  fprintf(fp,"%d %d:",vertical,side);

  for(i = 0;i < vertical;i++){
    for(j = 0;j < side;j++){
      fprintf(fp,"%d ",data[i][j]);
    }
    fprintf(fp,":");
  }

  fprintf(fp,"%d %d:%d %d:",rand()%vertical-1,rand()%side-1,rand()%vertical-1,rand()%side-1);

  fclose(fp);
}

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

int main(int argc,char* argv[]){
  if(argc < 5){
    fprintf(stderr,"入力データが不足しています。\n");
    fprintf(stderr,"%s <vertical> <side> <type> <outfilename>\n",argv[0]);
    fprintf(stderr,"type = 1:左右対称 2:上下対称 3:上下左右対称\n");
    exit(1);
  }

  int vertical = atoi(argv[1]);
  int side = atoi(argv[2]);
  int type = atoi(argv[3]);

  int data[12][12];
  int i,j;
  int point;

  FILE *fp;

  srand((unsigned)time(NULL));

  if(type = 1){
    for(i = 0;i<vertical;i++){
      for(j = 0;j<=side/2;j++){
        point = (rand()%33)-16;
        data[i][j] = point;
        data[i][side-1-j] = point;
      }
    }
  }else if(type = 2){
    for(i = 0;i<=vertical/2;i++){
      for(j = 0;j<side;j++){
        point = (rand()%33)-16;
        data[i][j] = point;
        data[vertical-1-i][j] = point;
      }
    }
  }else if(type = 1){
    for(i = 0;i<=vertical/2;i++){
      for(j = 0;j<=side/2;j++){
        point = (rand()%33)-16;
        data[i][j] = point;
        data[vertical-1-i][side-1-j] = point;
      }
    }
  }

  fp = fopen(argv[4],"w");

  fprintf(fp,"%d %d:",vertical,side);

  for(i = 0;i < vertical;i++){
    for(j = 0;j < side;j++){
      fprintf(fp,"%d ",data[i][j]);
    }
    fprintf(fp,":");
  }

  fprintf(fp,"%d %d:%d %d:",rand()%vertical-1,rand()%side-1,rand()%vertical-1,rand()%side-1);

  fclose(fp);

  return 0;
}

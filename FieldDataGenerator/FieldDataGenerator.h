#ifndef _FIELDDATAGENERATOR_H_
#define _FIELDDATAGENERATOR_H_

int opterr = 1;
int optind = 1;
int optopt = 0;
char* optarg;

char getopt(int argc,char** argv,const char* optstring){
  int i;

  opterr = 1;
  optind = 1;
  optopt = 0;

  while(optind < argc){
    if(argv[optind][0] == '-')
    printf("ok");

    optind++;
  }

  return optopt;
}

#endif

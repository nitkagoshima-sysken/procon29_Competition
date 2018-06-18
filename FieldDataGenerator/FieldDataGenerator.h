#ifndef _FIELDDATAGENERATOR_H_
#define _FIELDDATAGENERATOR_H_

int opterr = 1;
int optind = 1;
int optopt = 0;
char* optarg;

char getopt(int argc,char** argv,const char* optstring){
  int i;
  char result;

  opterr = 1;
  optopt = 0;

  while(optind < argc){
    if(argv[optind][0] == '-'){
      result = argv[optind][1];
      optarg = argv[optind+1];
      optind += 2;
      return result;
    }

    optind++;
  }

  return -1;
}

#endif

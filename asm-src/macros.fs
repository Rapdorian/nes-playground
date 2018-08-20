require binfile.fs
require globals.fs

vocabulary macros
macros definitions

variable mem_offset
variable mode

: PC outfile seek-pos mem_offset @ + ;

: org ( skip to n bytes ) 
	PC - 0 
	?do 0 outfile write-byte loop ;

: advance outfile seek-pos - mem_offset ! ;

: txt 1 mode !  ;
: data 0 mode ! ;

: byte  ( n ) mode @ if outfile write-byte else PC 1 + advance then ;
: fill ( n n -- ) 0 ?do dup byte loop ;
: word  ( n ) mode @ if outfile write-word else PC 2 + advance then ;
: dword ( n ) mode @ if outfile write-dword else PC 4 + advance then ;
: ascii ( c-addr u ) mode @ if outfile write-file throw else PC + advance drop then ;
: bin parse-word slurp-file outfile write-file throw ;
: $ PC constant ;

forth

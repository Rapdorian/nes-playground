vocabulary :spr
:spr definitions

create spr-stack 8 allot
variable spr-sp

: >spr ( n -- )
	spr-stack spr-sp @ cells + !
	1 spr-sp +!
;

: spr> ( -- n )
	-1 spr-sp +!
	spr-stack spr-sp @ cells + @
;

: bitc case
	0 of $00 endof
	1 of $01 endof
	2 of $10 endof
	3 of $11 endof
	endcase
;


: linec 0 8 0 ?do swap bitc 7 i - lshift or loop ;

: sprc 8 0 ?do linec >spr loop ;

: plane1
8 0 do 
	spr> $0F and byte
loop 8 spr-sp !
;

: plane2 8 0 do spr> $F0 and byte loop ;
: roll-over 
0 constant .
1 constant #
2 constant %
3 constant -

: ;spr  
sprc \ compile sprite
plane1\ write plane 1
plane2 \ write plane 2
previous \ revert word definitions
;

forth definitions

:spr
	. . . . . # # # 
	. . . . # # # #
	. . - . # % % %
	. . - . % % % %
	. . - - % - # -
	. . - - % - % -
	. . . - - - - -
	. . . . # - - %
;spr

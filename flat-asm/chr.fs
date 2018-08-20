txt
0 1024 8 * fill

vocabulary :spr
:spr definitions

: bitc case
	0 of $00 endof
	1 of $01 endof
	2 of $10 endof
	3 of $11 endof
	endcase
;


: linec 0 8 0 ?do swap bitc 7 i - lshift or loop ;
0 constant .
1 constant #
2 constant %
3 constant -

: ;spr previous ;

forth definitions

:spr
	. . . . . # # # linec
	. . . . # # # # linec
	. . - . # % % % linec
	. . - . % % % % linec
	. . - - % - # - linec
	. . - - % - % - linec
	. . . - - - - - linec
	. . . . # - - % linec
;spr

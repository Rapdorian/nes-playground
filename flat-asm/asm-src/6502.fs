require binfile.fs
require globals.fs

vocabulary asm-6502
asm-6502 definitions

0  constant  acc
1  constant  imm
2  constant  zp
3  constant  abs
4  constant  rel
5  constant  ind
6  constant  zp,x
7  constant  zp,y
8  constant  abs,x
9  constant  abs,y
10 constant ind,x
11 constant ind,y

forth definitions
asm-6502

: combine ( aaa bbb cc -- aaabbbcc ) swap 2 lshift or swap 5 lshift or ;
: byte-arg outfile write-byte outfile write-byte ;
: word-arg outfile write-byte outfile write-word ;
: no-arg outfile write-byte ;

: cc00 ( aaa adr -- bbb )
	swap
	case
	imm of %000 %00 combine byte-arg endof
	zp of %001 %00 combine byte-arg endof
	abs of %011 %00 combine word-arg endof
	zp,x of %101 %00 combine byte-arg endof
	abs,x of %111 %00 combine byte-arg endof
	." ERROR: INVALID ADDRESS MODE " cr bye
	endcase
;

: cc01 ( aaa adr -- bbb )
	swap
	case
	ind,x of %000 %01 combine word-arg endof
	zp    of %001 %01 combine byte-arg endof
	imm   of %010 %01 combine byte-arg endof
	abs   of %011 %01 combine word-arg endof
	ind,y of %100 %01 combine word-arg endof
	abs,y of %110 %01 combine word-arg endof
	abs,x of %111 %01 combine word-arg endof
	." ERROR: INVALID ADDRESS MODE " cr bye
	endcase
;

: cc10 ( aaa adr -- bbb )
	swap
	case
	imm   of %000 %10 combine byte-arg endof
	zp    of %001 %10 combine byte-arg endof
	acc   of %010 %10 combine no-arg endof
	abs   of %011 %10 combine word-arg endof
	zp,x  of %101 %10 combine byte-arg endof
	abs,x of %111 %10 combine word-arg endof
	." ERROR: INVALID ADDRESS MODE " cr bye
	endcase
;

: sign ( a -- s ) 0 < if -1 else 1 then ;

: signed ( a -- sa ) 
dup abs swap sign 0 < if
	invert 1 + $FF and else
	%01111111 and then
;

: br-op ( off xx y -- ) 
5 lshift swap 6 lshift or %10000 or \ build opcode
signed byte-arg
;

: change-adr { a o n -- new } a o = if n else a then ;

asm-6502 definitions

: ora ( v adr -- ) %000 cc01 ;
: and ( v adr -- ) %001 cc01 ;
: eor ( v adr -- ) %010 cc01 ;
: adc ( v adr -- ) %011 cc01 ;
: sta ( v adr -- ) %100 cc01 ;
: lda ( v adr -- ) %101 cc01 ;
: cmp ( v adr -- ) %110 cc01 ;
: sbc ( v adr -- ) %111 cc01 ;

: asl ( v adr -- ) %000 cc10 ;
: rol ( v adr -- ) %001 cc10 ;
: lsr ( v adr -- ) %010 cc10 ;
: ror ( v adr -- ) %011 cc10 ;
: stx ( v adr -- ) zp,y zp,x change-adr %100 cc10 ;
: ldx ( v adr -- ) zp,y zp,x change-adr abs,y abs,x change-adr %101 cc10 ;
: dec ( v adr -- ) %110 cc10 ;
: inc ( v adr -- ) %111 cc10 ;

: bit ( v adr -- ) %001 cc00 ;
: sty ( v adr -- ) %100 cc00 ; 
: ldy ( v adr -- ) %101 cc00 ;
: cpy ( v adr -- ) %110 cc00 ;
: cpx ( v adr -- ) %111 cc00 ;

: bpl %00 0 br-op ;
: bmi %00 1 br-op ;
: bvc %01 0 br-op ;
: bvs %01 1 br-op ;
: bcc %10 0 br-op ;
: bcs %10 1 br-op ;
: bzc %11 0 br-op ;
: bzs %11 1 br-op ;

' bzc alias bne
' bzs alias beq

' bmi alias blz
' bpl alias bgz
' bzc alias bnz
' bzs alias bez

: brk $00 no-arg ;
: jsr $20 imm = throw word-arg ;
: rti $40 no-arg ;
: rts $60 no-arg ;

: php $08 no-arg ;
: plp $28 no-arg ;
: pha $48 no-arg ;
: pla $68 no-arg ;
: dey $88 no-arg ;
: tay $a8 no-arg ;
: iny $c8 no-arg ;
: inx $e8 no-arg ;
: clc $18 no-arg ;
: sec $38 no-arg ;
: cli $58 no-arg ;
: sei $78 no-arg ;
: tya $98 no-arg ;
: clv $b8 no-arg ;
: cld $d8 no-arg ;
: sed $f8 no-arg ;
: txa $8a no-arg ;
: txs $9a no-arg ;
: tax $aa no-arg ;
: tsx $ba no-arg ;
: dex $ca no-arg ;
: nop $ea no-arg ;

: jmp ( v adr -- )  
case
	imm of %010 %011 %00 combine word-arg endof
	ind of %011 %011 %00 combine word-arg endof
	." ERROR: JMP Must be either imm or ind" cr bye
endcase
;

forth definitions

forth
\ data
variable MAPPER
variable SUB_MAPPER
\ sizes are in 1k
variable PRG_ROM 
variable CHR_ROM 
\ sizes are as powers of two starting at 7
variable PRG_RAM 
variable PRG_BRAM
variable CHR_RAM
variable CHR_BRAM

\ flags
variable FLAGS
: flag FLAGS @ or FLAGS ! ;

\ byte 6
$00 constant MIRR_HORZ
$01 constant MIRR_VERT
$02 constant BATT_RAM
$04 constant FLAG_TRAIN
$08 constant MIRR_FULL
\ byte 7
$0100 constant VS_ARCADE
$0200 constant PC_ARCADE
\ byte 12
$000000 constant TV_NTSC
$010000 constant TV_PAL
$020000 constant TV_DUAL

: PRG_POS FLAG_TRAIN FLAGS @ and if 528 else 16 then ;
: CHR_POS PRG_POS PRG_ROM @ 1024 * + ;
: END_POS CHR_POS CHR_ROM @ 1024 * + ;	

: flag_6 
	MAPPER @ $F and 4 lshift
	FLAGS @ $FF and 
	or
;

: flag_7 
	MAPPER @ $F0 and 
	FLAGS @ $FF00 and 8 rshift 
	8 or \ nes 2.0 id
	or
;

: byte_8
	SUB_MAPPER @ $F and 4 lshift
	MAPPER @ $F00 and 8 rshift
	or
;

: byte_9
	CHR_ROM @ 8  / $F00 and	4 rshift
	PRG_ROM @ 16 / $F00 and 8 rshift
	or
;

: byte_10
	PRG_BRAM @ $F and 4 lshift
	PRG_RAM @ $F
	or
;

: byte_11
	CHR_BRAM @ $F and 4 lshift
	CHR_RAM @ $F
	or
;

: byte_12 FLAGS @ $FF0000 16 rshift ;

: byte_13 0 ;

: byte_14 0 ;

also macros
also asm-6502

: nes2.0
	txt
	s\" NES\x1A" 	ascii 
	PRG_ROM @ 16 / 	byte
	CHR_ROM @ 8  / 	byte
	flag_6		byte
	flag_7		byte
	byte_8		byte
	byte_9		byte
	byte_10		byte
	byte_11		byte
	byte_12		byte
	byte_13		byte
	byte_14		byte
	0		byte
	0 advance
;





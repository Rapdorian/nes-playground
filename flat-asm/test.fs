require neslib.fs
require mappers/nrom.fs

nrom-128

data
$ BG	byte

txt
$C000 advance

$ start
	$80 imm PPUCTRL
$ a	a imm jmp

$ nmi	BG  zp  inc
	$3F imm PPUADDR
	$00 imm PPUADDR
	$00 zp  PPUDATA
	rti

$FFFA org
	nmi word
	start word
	$0000 word

include chr.fs

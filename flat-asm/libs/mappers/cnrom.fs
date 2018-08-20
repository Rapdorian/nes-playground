include ../nes2.fs

: cnrom-128 
	3 MAPPER !
	16 PRG_ROM !
	32 CHR_ROM !
	nes2.0
;

: cnrom-256
	3 MAPPER !
	32 PRG_ROM !
	32 CHR_ROM !
	nes2.0
;

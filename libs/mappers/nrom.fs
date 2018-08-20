include nes2.fs

: nrom-128 
	0 MAPPER !
	16 PRG_ROM !
	8 CHR_ROM !
	nes2.0
;

: nrom-256
	0 MAPPER !
	32 PRG_ROM !
	8 CHR_ROM !
	nes2.0
;

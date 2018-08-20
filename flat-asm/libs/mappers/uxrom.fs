include ../nes2.fs

: unrom-512
	2 MAPPER !
	64 PRG_ROM !
	8 CHR_ROM !
	nes2.0
;

: unrom-1024
	2 MAPPER !
	128 PRG_ROM !
	8 CHR_ROM !
	nes2.0
;

: uorom
	2 MAPPER !
	256 PRG_ROM !
	8 CHR_ROM !
	nes2.0
;


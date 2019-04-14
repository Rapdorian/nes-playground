.segment "HEADER"
NES_PRG_BANKS = 2
NES_CHR_BANKS = 8
NES_MIRRORING = 0
NES_MAPPER = 3

.byte $4e, $45, $53, $1a
.byte <NES_PRG_BANKS
.byte <NES_CHR_BANKS
.byte <NES_MIRRORING|(<NES_MAPPER<<4)
.byte <NES_MAPPER&$f0
.res 8,0

.include "test.s"
main:
    sta $2000
    lda #$1e
    sta $2001
    ldx #$FF-4

    jmp foo
    jmp loop

nmi:
    rti
irq:
    jmp main
.segment "VECTORS"
.word nmi, main, irq


.zeropage
ip: .res 1
ip_h: .res 1
scratch: .res 2
low_table: .res 252
.bss
high_table: .res 256

.rodata
next:
    iny
    lda (ip), y
    pha
    iny
    lda (ip), y
    pha
    php
    rti

return:
    pla
    tay
    pla
    sta ip
    pla
    sta ip_h
    jmp next

.macro do_col
    lda ip+1
    pha
    lda ip
    pha
    tya
    pha
    lda #<(@end-1)
    sta ip
    lda #>(@end-1)
    sta ip+1
    ldy #0
    jmp next
    @end:
.endmacro

add_byte:
    lda low_table, x
    clc
    inx
    adc low_table, x
    sta low_table, x
    jmp next

push_byte:
    iny
    dex
    lda (ip), y
    sta low_table, x
    lda #0
    sta high_table, x
    jmp next

push_word:
    iny
    dex
    lda (ip), y
    sta high_table, x
    iny
    lda (ip), y
    sta low_table, x
    jmp next

store_byte:
    tya
    pha
    ldy low_table, x
    lda high_table, x
    sta scratch+1
    inx
    lda low_table, x
    sta (scratch), y
    pla
    tay
    jmp next

loop:
    jmp loop

.include "nes.s"

feed_word:
    tya
    pha
    ldy low_table, x
    lda high_table, x
    sta scratch+1
    inx
    lda low_table, x
    sta (scratch), y
    lda high_table, x
    sta (scratch), y
    pla
    tay
    jmp next

foo: do_col
    .dbyt push_word, $3F00, push_word, PPU_ADDR, feed_word
    .dbyt push_byte, $13, push_word, PPU_DATA, store_byte, loop

#! /bin/gforth

require 6502.fs
require macros.fs
require globals.fs

fpath path+ libs

next-arg 
next-arg w/o bin create-file throw set-outfile

also macros
also asm-6502

include compat/required.fs
required

outfile close-file throw
bye

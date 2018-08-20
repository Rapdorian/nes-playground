require xo65/write/segment.fs
require xo65/write/header.fs
require xo65/write/expr.fs
require xo65/write/stringpool.fs

: new-file
parse-word r/w bin create-file throw 
dup write-empty-header
4 0 ?do 0 over write-byte loop
;

: start-segments { objfile -- }
objfile seek-pos segments offset !
seg-count @ objfile write-byte \ number of segments
s" CODE" objfile new-seg
;

new-file test.o 
constant objfile

objfile start-segments

 expr 0 SECT 3 LIT + end-expr
1 new-expr 

objfile push-frag

objfile write-string-pool

objfile write-header-table

objfile flush-file
objfile close-file
bye

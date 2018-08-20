require ../common/binfile.fs
require ../common/format.fs
require fragment.fs
require stringpool.fs

\ block vars
variable seg-count

\ segment vars
variable seg-offset
variable seg-length
variable code-length
variable frag-length
variable seg-align
variable seg-addrw
variable seg-name

: seek-seg-len   { objfile } seg-offset @ objfile seek ;
: seek-seg-name  { objfile } seg-offset @ 4 + objfile seek ;
: seek-code-len  { objfile } seg-offset @ 6 + objfile seek ;
: seek-seg-align { objfile } seg-offset @ 7 + objfile seek ;
: seek-seg-addrw { objfile } seg-offset @ 8 + objfile seek ;
: seek-frag-len  { objfile } seg-offset @ 9 + objfile seek ;
: seek-seg-end   { objfile } seg-offset @ seg-length @ + objfile seek ;

: read-seg-len   ( objfile -- n ) dup seek-seg-len read-dword ;
: read-seg-name  ( objfile -- n ) dup seek-seg-name read-byte ;
: read-code-len  ( objfile -- n ) dup seek-code-len read-byte ;
: read-frag-len  ( objfile -- n ) dup seek-frag-len read-byte ;
: read-seg-align ( objfile -- n ) dup seek-seg-align read-byte ;
: read-seg-addrw ( objfile -- n ) dup seek-seg-addrw read-byte ;

: write-seg-len   ( n objfile -- ) dup seek-seg-len write-dword ;
: write-seg-name  ( n objfile -- ) dup seek-seg-name write-byte ;
: write-code-len  ( n objfile -- ) dup seek-code-len write-byte ;
: write-seg-align ( n objfile -- ) dup seek-seg-align write-byte ;
: write-seg-addrw ( n objfile -- ) dup seek-seg-addrw write-byte ;
: write-frag-len  ( n objfile -- ) dup seek-frag-len write-byte ;

: read-segstate { objfile } 
objfile read-seg-len  seg-length  !
objfile read-seg-name seg-name !
objfile read-code-len code-length !
objfile read-frag-len frag-length !
objfile read-seg-align seg-align !
objfile read-seg-addrw seg-addrw !
;

: write-segstate { objfile }
seg-length @ objfile write-seg-len
seg-name @ objfile write-seg-name
code-length @ objfile write-code-len
frag-length @ objfile write-frag-len
seg-align @ objfile write-seg-align
seg-addrw @ objfile write-seg-addrw
;

: seg-block-len { objfile }
	segments offset @ objfile seek	
	1 objfile read-byte 0 ?do
		objfile read-dword
		dup objfile seek-ahead 
		+
	loop
;

: new-seg { objfile }
	objfile seek-pos seg-offset !
	10 0 ?do 0 objfile write-byte loop
	10 seg-length !
	1 seg-align !
	2 seg-addrw !
	new-string seg-name !
	objfile write-segstate

	1 seg-count +!
	segments offset @ objfile seek
	seg-count @ objfile write-byte

	objfile seg-block-len segments size !
;


." WARNING!: LineInfo not implemented" cr
: push-frag ( [frag] objfile -- ) { objfile }
	objfile seek-seg-end
	\ update segment state
	frag-code-len code-length +!
	1 frag-length +!
	frag-total-len seg-length +!
	\ write frag
	objfile write-byte \ frag-type
	0 ?do
		objfile write-byte \ expr data
	loop
	
	1 objfile write-byte
	0 objfile write-byte
	2 seg-length +!

	objfile write-segstate 
	objfile seg-block-len segments size !
;

\ TODO
\ * New Segment ( with name )

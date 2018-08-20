require ../common/format.fs
require ../common/binfile.fs
\ we need to store the following
create string-pool[ 255 allot
variable string-count

: s] cells + ;
: c] chars + ;

: new-string { c-addr u -- n }
	u 1 + allocate throw { str }
	u str c!
	c-addr str 1 + u cmove \ copy c-addr into str+1
	str string-pool[ string-count @ s] !
	1 string-count +!
	string-count @ 1 -
;

: get-string { n -- c-addr u }
	string-pool[ n s] @ 1 + \ string-pool[n] + 1
	string-pool[ n s] @ c@
;

\ returns the number of bytes written
: write-string { c-addr[ u objfile -- n }
	u objfile write-byte
	u 0 ?do c-addr[ i c] c@ objfile write-byte loop
	u 1 +
;

\ now we need to spit out the block table
: write-string-pool { objfile }
	objfile file-size throw objfile reposition-file throw
	\ change string-pool offset
	objfile seek-pos string-pool offset !
	\ write number of strings
	string-count @ objfile write-byte
	\ write out strings
	1 string-count @ 0 ?do
		i get-string objfile write-string +
	loop
	\ change string-pool size
	string-pool size !
;



: read-byte ( wfileid -- n ) key-file ;
: read-word ( wfileid -- n ) dup read-byte swap read-byte 8 lshift + ;
: read-dword ( wfileid -- n ) dup read-word swap read-word 16 lshift + ;

: push-byte { b c-addr u -- c-addr u }
	b $FF and 
	c-addr u + c!
	c-addr u 1 +
;

: push-word { b c-addr u -- c-addr u }
	b c-addr u push-byte { c-addr2 u2 }
	b 8 rshift c-addr2 u2 push-byte ;

: push-dword { b c-addr u -- c-addr u }
	b c-addr u push-word { c-addr2 u2 }
	b 16 rshift c-addr2 u2 push-word ;

: write-byte ( b wfileid -- ) swap pad 0 push-byte rot write-file throw ;
: write-word ( b wfileid -- ) swap pad 0 push-word rot write-file throw ;
: write-dword ( b wfileid -- ) swap pad 0 push-dword rot write-file throw ;	

: seek-pos ( wfileid -- n ) file-position throw d>s ;
: seek ( n wfileid -- ) swap s>d rot reposition-file throw ;
: seek-ahead ( n wfileid ) dup seek-pos rot + swap seek ;

: skip-bytes ( n wfileid -- ) 
	tuck seek-pos + ( wfileid n ) 
	swap seek ;
: skip-byte ( wfileid -- ) 1 swap skip-bytes ;
: skip-word ( wfileid -- ) 2 swap skip-bytes ;
: skip-dword ( wfileid -- ) 4 swap skip-bytes ;


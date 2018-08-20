require ../common/bytes.fs
\ Let's have fragments generated in memory and then passed to the segment code
\ for writing

\ Mask Definitions 
$38 constant FRAG-TYPE-MASK
$07 constant FRAG-BYTE-MASK

: new-expr ( [expr] size -- [data] type ) $08 or ;
: new-sexpr ( [expr] size -- [data] type ) $10 or ;
: new-lit (  n -- [data] type] ) ( TODO ) dup width >r to-bytes r> dup 00 ; 

: expr-code-len dup FRAG-BYTE-MASK and ;
: lit-code-len over ;

: expr-total-len over 1 + ;
: lit-total-len lit-code-len 2 + ;

: frag-total-len 
	dup FRAG-TYPE-MASK and case
		$08 of expr-total-len endof
		$10 of expr-total-len endof
		$00 of lit-total-len endof
	endcase
;

: frag-code-len
	dup FRAG-TYPE-MASK and case
		$08 of expr-code-len endof
		$10 of expr-code-len endof
		$00 of lit-code-len endof
	endcase
;

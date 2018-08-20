
\ Mask definitions
$0C constant EXPR-TYPE-MASK
$3F constant EXPR-OP-MASK

variable expr-len

: LEAF  $80 or ;
: BIOP  $00 or ;
: UNOP  $40 or ;

vocabulary expr
expr definitions


1 LEAF constant LIT \ should make this spread into bytes
2 LEAF constant SYM
3 LEAF constant SECT

$01 BIOP constant +
$02 BIOP constant -
$03 BIOP constant *
$04 BIOP constant /
$05 BIOP constant %
$06 BIOP constant |
$07 BIOP constant ^
$08 BIOP constant &
$09 BIOP constant <<
$0A BIOP constant >>
$0B BIOP constant =
$0C BIOP constant !=
$0D BIOP constant <
$0E BIOP constant >
$0F BIOP constant <=
$10 BIOP constant >=
$11 BIOP constant &&
$12 BIOP constant ||
$13 BIOP constant ^^
$14 BIOP constant MAX
$15 BIOP constant MIN

01 UNOP constant NEG
02 UNOP constant !
03 UNOP constant SWAP \ I don't actually know the behaviour of this op
04 UNOP constant !!
05 UNOP constant BANK \ I don't actually know the behaviour of this op
08 UNOP constant B0
09 UNOP constant B1
10 UNOP constant B2
11 UNOP constant B3
12 UNOP constant W0
13 UNOP constant w1
14 UNOP constant FAR \ I don't actually know the behaviour of this op
15 UNOP constant DWORD \ I don't actually know the behaviour of this op

forth
forth definitions

: expr depth expr-len ! expr ;
: end-expr depth expr-len @ - forth ;

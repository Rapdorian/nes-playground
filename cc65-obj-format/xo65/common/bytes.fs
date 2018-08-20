
: get-byte ( n n -- ) 8 * rshift $FF and ;
: width ( n ) 255 / 1 + ;
: to-bytes ( n -- [bytes] ) dup width 0 ?do dup i get-byte swap loop drop ;

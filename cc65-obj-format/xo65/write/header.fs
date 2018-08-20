require ../common/binfile.fs
require ../common/format.fs

08 constant options-table
16 constant files-table
24 constant segments-table
32 constant imports-table
40 constant exports-table
48 constant debug-table
56 constant lineinfo-table
64 constant strings-table
72 constant assert-table
80 constant scope-table

: write-preamble { wfileid -- }
	$616E7A55 wfileid write-dword 	\ magic number
	17 wfileid write-word		\ version
	0 wfileid write-word		\ flags
;

: write-table-entry ( offset size wfileid -- )
	rot over 
	write-dword write-dword
;

: seek-table ( table wfileid -- wfileid ) tuck seek ;

: write-offset ( offset table wfileid -- ) seek-table write-dword ;

: write-size ( size table wfileid ) seek-table skip-dword write-dword ;

: write-header-table { wfileid -- }
	options-table wfileid seek
	options     2@ swap wfileid write-table-entry
	files       2@ swap wfileid write-table-entry
	segments    2@ swap wfileid write-table-entry
	imports     2@ swap wfileid write-table-entry
	exports     2@ swap wfileid write-table-entry
	debug-sym   2@ swap wfileid write-table-entry
	line-info   2@ swap wfileid write-table-entry
	string-pool 2@ swap wfileid write-table-entry
	assertions  2@ swap wfileid write-table-entry
	scopes      2@ swap wfileid write-table-entry
;


: write-empty-header { wfileid -- }
	wfileid write-preamble
	10 0 ?do
		$58 1 wfileid write-table-entry
	loop
\	2 0 ?do 0 wfileid write-dword loop

	$58 options offset !
	$58 files offset !
	$58 segments offset !
	$58 imports offset !
	$58 exports offset !
	$58 debug-sym offset !
	$58 line-info offset !
	$58 string-pool offset !
	$58 assertions offset !
	$58 scopes offset !

	1 options     size !
	1 files       size !
	1 segments    size !
	1 imports     size !
	1 exports     size !
	1 debug-sym   size !
	1 line-info   size !
	1 string-pool size !
	1 assertions  size !
	1 scopes      size !
;

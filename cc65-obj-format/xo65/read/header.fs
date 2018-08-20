require ../common/format.fs
require ../common/binfile.fs

: slurp-magic ( wfileid -- )
	read-dword $616E7A55 = invert
	abort" Failed to read magic number! "
;

: read-header-value { fileid a -- }
	fileid read-dword a offset !
	fileid read-dword a size !
;

: read-header { fileid -- }
	fileid slurp-magic
	fileid read-word version !
	fileid read-word flags !

	fileid options read-header-value
	fileid files read-header-value
	fileid segments read-header-value
	fileid imports read-header-value
	fileid exports read-header-value
	fileid debug-sym read-header-value
	fileid line-info read-header-value
	fileid string-pool read-header-value
	fileid assertions read-header-value
	fileid scopes read-header-value
;

: show-header-value { hdr -- }
		."     offset: " hdr @ hex. cr
		."     size: " hdr size @ hex. cr cr 
;

: show-header ( -- )
	." options: " cr options show-header-value
	." files: " cr files show-header-value
	." segments: " cr segments show-header-value
	." imports: " cr imports show-header-value
	." exports: " cr exports show-header-value
	." debug symbols: " cr debug-sym show-header-value
	." line info: " cr line-info show-header-value
	." string pool: " cr string-pool show-header-value
	." assertions: " cr assertions show-header-value
	." scopes: " cr scopes show-header-value
	." END-HEADER" cr cr
;

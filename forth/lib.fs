\ Tue 14 Feb 19:10:10 UTC 2023

hex 1 \ pad

: <1?  ( n -- BOOL )
  dup 1 - 0< if drop -1 exit then drop 0 ;

\ 1 2 << ( gives '4' )

: << ( n shifts -- shifted )
  lshift ;

\ 2^0 = 1 \ 2^1 = 2 \ 2^2 = 4

: 2^ ( n -- 2^n )
  dup <1?  0< if
    drop 1 exit
  then
  1 swap << ;

: delay ( n -- ) \ unspecified absolute time interval TODO: measure, specify
  depth 1 - 0< if exit then
  1 max \ 1 seems enough for a do .. loop
  0 do 3 0 do 11 0 do 100 0 do
      1 drop
  loop loop loop loop ;

\ 14 Feb 2023 19:10z
\ END.

# Object comparer
Compares two objects - single comparison and objects inside a list.

## Description
Measures how long it takes to compare objects

FIRST VERSION: Compares array of bytes

## Comparisons used: 
> CompareByteArrays_Utf8ToString
> CompareByteArrays_SpanSequenceEquals
> CompareByteArrays_SequenceEquals
> CompareByteArrays_Operator
> CompareByteArrays_Equals         

## Execution Results
> Method:         CompareByteArrays_Utf8ToString           Found      - Execution took: 00:00:00.0008335
> Method:         CompareByteArrays_SpanSequenceEquals     Found      - Execution took: 00:00:00.0038750
> Method:         CompareByteArrays_SequenceEquals         Found      - Execution took: 00:00:00.0038444
> Method:         CompareByteArrays_Operator               Not found  - Execution took: 00:00:00.0000767
> Method:         CompareByteArrays_Equals                 Not found  - Execution took: 00:00:00.0000592

> SEARCH Method:  CompareByteArrays_Utf8ToString           Found      - Execution took: 00:00:06.1757652
> SEARCH Method:  CompareByteArrays_SpanSequenceEquals     Found      - Execution took: 00:00:00.6263261
> SEARCH Method:  CompareByteArrays_SequenceEquals         Found      - Execution took: 00:00:00.8535329
> SEARCH Method:  CompareByteArrays_Operator               Not found  - Execution took: 00:00:00.4228819
> SEARCH Method:  CompareByteArrays_Equals                 Not found  - Execution took: 00:00:00.4742452

## License
[GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.html)


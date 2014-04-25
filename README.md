Bulk Insert Benchmark
=====================

A quick and dirty benchmark to compare the performance of the following methods of inserting lots of data:

- SqlBulkCopy (BCP)
- Serializing the data to XML, passed into a stored proc
- Individual inserts (shared connection)

Here are the results on my machine:


<table>
	<tr>
		<th></th>
		<th colspan="2">SqlBulkCopy</th>
		<th colspan="2">XML</th>
		<th colspan="2">Standard Inserts</th>
	</tr>
	<tr>
		<th># of Items</th>
		<th>Time (ms)</th>
		<th>Items/s</th>
		<th>Time (ms)</th>
		<th>Items/s</th>
		<th>Time (ms)</th>
		<th>Items/s</th>
	</tr>
	<tr>
		<td>50</td><td> 13 </td><td> 19231 </td><td> 18 </td><td> 13889 </td><td> 116 </td><td> 2155 </td>
	</tr>
	<tr>
		<td>100</td><td> 16 </td><td> 31250 </td><td> 27 </td><td> 18519 </td><td> 228 </td><td> 2193 </td>
	</tr>
	<tr>
		<td>1000</td><td> 49 </td><td> 102041 </td><td> 215 </td><td> 23256 </td><td> 2215 </td><td> 2257 </td>
	</tr>
	<tr>
		<td>5000</td><td> 141 </td><td> 177305 </td><td> 1046 </td><td> 23901 </td><td> 11230 </td><td> 2226 </td>
	</tr>
	<tr>
		<td>10000</td><td> 242 </td><td> 206612 </td><td> 1999 </td><td> 25013 </td><td> 22015 </td><td> 2271 </td>
	</tr>
	<tr>
		<td>50000</td><td> 1079 </td><td> 231696 </td><td> 9837 </td><td> 25414 </td><td> 109936 </td><td> 2274 </td>
	</tr>
	<tr>
		<td>100000</td><td> 2174 </td><td> 229991 </td><td> 19531 </td><td> 25600 </td><td> 221162 </td><td> 2261 </td>
	</tr>
	<tr>
		<td>350000</td><td> 7867 </td><td> 222448 </td><td> 70564 </td><td> 24800 </td><td> 770604 </td><td> 2271 </td>
	</tr>
</table>

At best, BCP is **9.12x** faster than XML, **101.89x** faster than individual inserts.

At worst, BCP is **1.38x** faster than XML, **8.9x** faster than individual inserts.

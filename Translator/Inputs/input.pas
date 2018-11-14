program pr;
var
x, y: integer;
res1, res2, res3, res4: integer;
begin
	write('input x: ');
	readln(x);
	
	write('input y: ');
	readln(y);
	
	
  res1 := x + y;
  write('x + y = ');
  writeln(res1);
  
  res2 := x - y;
  write('x - y = ');
  writeln(res2);
  
  res3 := x * y;
  write('x * y = ');
  writeln(res3);
  
  res4 := x div y;
  write('x / y = ');
  writeln(res4);
end.

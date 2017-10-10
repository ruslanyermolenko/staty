#include <iostream>

int qb[6][6];
int dx[8] = {0, 3, 0, -3, 2, -2, 2, -2};
int dy[8] = {3, 0, -3, 0, 2, -2, -2, 2};
int n_v = 0;  
void rec(int x, int y, int num)
{
	qb[x][y] = num;
	if (num == 25)
	{
		++n_v;
		qb[x][y] = 0;
		std::cout << x << " " << y << "  " << num << "    " <<  n_v << std::endl;
		return;
	}
	for (int i = 0; i < 8; ++i)
	{
		if (x+dx[i] >0 && x+dx[i] < 6 && y +dy[i] >0 && y+dy[i] <6)
			if (qb[x + dx[i]][y + dy[i]] == 0)
			{
				rec(x + dx[i], y + dy[i], num + 1);
			}
	}
	qb[x][y] = 0;

}

int main()
{
	rec(1, 2, 1);

	std::cout << n_v; 

}
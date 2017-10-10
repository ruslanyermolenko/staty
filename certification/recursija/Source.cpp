#include <iostream>

int status[30];
int variant[10] = { 0,1,2,3,4,5,6,7,8,9 };
int N;
void rec( int n)
{
	if (n - 1 == N)
	{
		for (int i = 0; i < N; std::cout << status[++i]);
		std::cout << std::endl;
		return;
	}
	
	for (int k = 0; k < 10; ++k)
	{
		bool new1 = true;
		for (int k1 = 0; k1 < n; ++k1)
		{
			new1 = new1 && (status[k1] != variant[k]);
		}
		if (!new1)
		{
			continue;
		}
		status[n] = variant[k];
		rec(n + 1);
	}
	/*status[n] = 0;
	rec(n + 1);
	status[n] = 1;
	rec(n + 1);*/
}

int main()
{
	N = 2;
	rec(0);
}

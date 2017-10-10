#include <iostream>
#include <fstream>
#define buffsize 100

int dx[4] = { -1,1,0,0 };
int dy[4] = { 0, 0, -1, 1 };
	struct xy
	{
		xy()
		{
			x = 0;
			y = 0;
		}
		xy(int x_, int y_)
		{
			x = x_;
			y = y_;
		}
		int x;
		int y;
	};

class quere
{
public:
	quere()
	{
		first = 0;
		end = 0;
		num_elm = 0;
	};
	int first;
	int end;
	int num_elm;
	xy elem[buffsize];
	bool isEmpty()
	{
		return  !num_elm;
	};

	bool put(int x, int y)
	{
		if (num_elm == buffsize) return false;
		elem[end] = xy(x, y);
		++num_elm;
		end = (end + 1) % buffsize;
		return true;

	}
	xy get()
	{
		if (isEmpty()) return xy(-1,-1);
		xy res = elem[first];
		first = (first + 1) % buffsize;
		--num_elm;
		return res;
	}
	bool isFull()
	{
		return ((num_elm ) >= buffsize);
	}
};

#define labSize 5

int L[labSize + 2][labSize + 2];
quere Q[2];

void putQ(int x, int y,  int iQ)
{
	Q[iQ].put(x, y);
}



void req(int x, int y, int wave, int iQ)
{
	for (int i = 0; i < 4; ++i)
	{
		if (!L[x + dx[i]][y + dy[i]])
		{
			putQ(x + dx[i], y + dy[i],  iQ);
		}
	}
	L[x][y] = wave;
}

void Q_start(int iQ, int wave)
{
	while (!Q[iQ].isEmpty())
	{
		xy res = Q[iQ].get();
		req(res.x, res.y, wave, !iQ);
	}
}
void print()
{
	std::cout <<  "------------------------------" << std::endl;
	for (int i = 0; i <= labSize+1; ++i)
	{
		for (int j = 0; j <= labSize+1; ++j)
		{
			std::cout << L[i][j] << " ";
		}
		std::cout << std::endl;
	}
	std::cout << "------------------------------" << std::endl;
}

void recDFS(int x, int y, int step)
{
	L[x][y] = step;
	for (int i = 0; i < 4; ++i)
	{
		if (!L[x + dx[i]][y + dy[i]])
		{
			recDFS(x + dx[i], y + dy[i], step +1);
		}
	}
	

}

int main()
{
	//quere Q;
	//for (int i = 0; i <= 10 * buffsize; ++i)
	//{
	//	if (!Q.isFull())
	//	{
	//		Q.put(i);
	//	}
	//	else
	//	{
	//		while (!Q.isEmpty())
	//		{
	//			std::cout << Q.get() << std::endl;
	//		}
	//		Q.put(i);
	//	}
	//	
	//}

	std::ifstream F("d:/stady/lab.txt", 1);

	for (int i = 1; i <= labSize; ++i)
	{
		for (int j = 1; j <= labSize; ++j)
		{
			F >> L[i][j];
		}
	}

	for (int j = 0; j <= labSize; ++j)
	{
		 L[0][j] = 1;
		 L[j][0] = 1;
		 L[labSize+1][j] = 1;
		 L[j][labSize +1] = 1;
	}
	int w = 1;
	print();
//	recDFS(1, 1, 2);
	putQ(1, 1, 0);
	int iQ = 0;
	while (!Q[0].isEmpty() || !Q[1].isEmpty())
	{
		Q_start(iQ, ++w);
		iQ = !iQ;
		print();
	}

	print();
}


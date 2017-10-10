#include <iostream>
struct vertex
{
	int myindex;
	int num_node;
	int node[10];
	int W[10];
	int N_v_min_W;
};

vertex Graph[50];
int Vertex_use[10];
int num_use_vert = 0;
int W_ver[10];

void Cheak_vertax(int n_vert)
{
	for (int j = 0; j < num_use_vert; j++)
	{

		if (Vertex_use[j] == n_vert) return;
	}
	Vertex_use[num_use_vert] = n_vert;
	num_use_vert++;
	int w_c;
	for (int i = 0; i < Graph[n_vert].num_node; i++)
	{
		w_c = W_ver[n_vert] + Graph[n_vert].W[i];
		if (w_c < W_ver[Graph[n_vert].node[i]])
		{
			W_ver[Graph[n_vert].node[i]] = w_c;
			Graph[Graph[n_vert].node[i]].N_v_min_W = n_vert;
		}
	}

	for (int i = 0; i < Graph[n_vert].num_node; i++)
	{
		Cheak_vertax(Graph[n_vert].node[i]);
	}

}

void OutTreak(int n_g )
{
	if (n_g == 0) return;
	std::cout << "G#" << Graph[n_g].myindex << "W=" << W_ver[n_g] << std::endl;
	OutTreak(Graph[n_g].N_v_min_W);

}
int main(int narg, char *args)
{

	
	Graph[0].myindex = 0;
	Graph[0].num_node = 3;
	Graph[0].node[0] = 1;
	Graph[0].node[1] = 2;
	Graph[0].node[2] = 3;
	Graph[0].W[0] = 1;
	Graph[0].W[1] = 5;
	Graph[0].W[2] = 7;


	Graph[1].myindex = 1;
	Graph[1].num_node = 3;
	Graph[1].node[0] = 0;
	Graph[1].node[1] = 2;
	Graph[1].node[2] = 4;
	Graph[1].W[0] = 1;
	Graph[1].W[1] = 3;
	Graph[1].W[2] = 6;

	Graph[2].myindex = 2;
	Graph[2].num_node = 3;
	Graph[2].node[0] = 0;
	Graph[2].node[1] = 1;
	Graph[2].node[2] = 4;
	Graph[2].W[0] = 5;
	Graph[2].W[1] = 3;
	Graph[2].W[2] = 3;

	Graph[3].myindex = 3;
	Graph[3].num_node = 2;
	Graph[3].node[0] = 0;
	Graph[3].node[1] = 4;
	Graph[3].W[0] = 7;
	Graph[3].W[1] = 8;
	
	Graph[4].myindex = 4;
	Graph[4].num_node = 3;
	Graph[4].node[0] = 1;
	Graph[4].node[1] = 2;
	Graph[4].node[2] = 7;
	Graph[4].W[0] = 6;
	Graph[4].W[1] = 3;
	Graph[4].W[2] = 8;

	
	int num_vert = 5;
	W_ver[0] = 0;
	for (int i = 1; i < 5; i++)
	{
		W_ver[i] = 10000000;
	}

	for (int i = 0; i < num_vert; i++)
	{

		Vertex_use[i] = -1;
	}

	Cheak_vertax( 0);


	OutTreak(4);


	std::cout << "Hello" << std::endl;
	int a;
	std::cin >> a;
	return 0;
}
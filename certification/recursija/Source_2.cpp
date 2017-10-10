#include <iostream>


std::string str = "0123456789";
std::string res;

void Rec(int i)
{
	if (i >= 2)
	{
		std::cout << res.c_str() << std::endl;
		return;
	}
	for (int j = 0; j < str.length(); ++j)
	{
		//bool newLet = true;
		//for (int l = 0; l < i; ++l)
		//{
		//	newLet = newLet && !(res[l] == str[j]);
		//	if (!newLet)
		//	{
		//		break;
		//	}
		//}
		//if (!newLet) continue;

		res[i] = str[j];
		Rec(i+1);
	}
		

}

int main()
{
	res = "              ";
	Rec(0);

}
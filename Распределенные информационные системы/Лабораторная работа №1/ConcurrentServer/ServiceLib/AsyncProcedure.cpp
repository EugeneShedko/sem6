#include "pch.h"
#include "Services.h"
#include <chrono>
#include <string>

namespace services
{
	string get_ip_port_string_from_sockaddr_in(const SOCKADDR_IN sockaddr)
	{
		char ip[INET_ADDRSTRLEN];
		inet_ntop(AF_INET, &sockaddr.sin_addr, ip, INET_ADDRSTRLEN);
		auto port = ntohs(sockaddr.sin_port);
		string result = { ip };
		result += ':' + std::to_string(port);
		return result;
	}

	void CALLBACK services::StartServingAsync(Contact* contact)
	{
		const auto buffer = get_current_time();
		SOCKADDR_IN addr{};
		int addr_size = sizeof addr;

		cout << "Started serving " << get_ip_port_string_from_sockaddr_in(addr) << " " << buffer;
	}

	void CALLBACK services::FinishServingAsync(Contact* contact)
	{
		const auto buffer = get_current_time();
		cout << "Finished serving client " << buffer;
	}
}

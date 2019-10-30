package serverutils

import (
	"net"
)

// GeneratePort() generates a unused port number
// which can be binded to by a server
func GeneratePort() int {
	// :0 Ask's Kernal to automatically bind to a unused port
	listen, _ := net.Listen("tcp", ":0")
	listen.Close()                           // Close listener
	return listen.Addr().(*net.TCPAddr).Port // Return the port given by the Kernal
}

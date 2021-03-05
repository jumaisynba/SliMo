#!/usr/bin/env python
import socket
import sys
import rospy
import os

#import json
from geometry_msgs.msg import Wrench, WrenchStamped
from std_msgs.msg import Float64, String

# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the port



server_address = ("192.168.1.104", 9090)
print(sys.stderr, "starting up on %s port %s" % server_address)


sock.bind(server_address)

# Listen for incoming connections


sock.listen()

print(sys.stderr, "waiting for a connection")
connection, client_address = sock.accept()
print(sys.stderr, "connection from", client_address)


def nodo():
	
	pub = rospy.Publisher('modder', String, queue_size =10)
	rospy.init_node('wearam', anonymous=True)
	while not rospy.is_shutdown():
		mode = connection.recv(1024)

		pub.publish(mode.decode("utf-8"))
		if not mode:
			break
		if (mode.decode("utf-8") !=0):
			print(mode.decode("utf-8"))

	#rospy.spin()

	

	

if __name__ == '__main__':
	try:
		nodo()
	except rospy.ROSInterruptException:
		pass
	

    

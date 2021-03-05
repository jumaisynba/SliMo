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



server_address = ("192.168.1.104", 8052)
print(sys.stderr, "starting up on %s port %s" % server_address)


sock.bind(server_address)

# Listen for incoming connections


sock.listen()

print(sys.stderr, "waiting for a connection")
connection, client_address = sock.accept()
print(sys.stderr, "connection from", client_address)


class Nodo():

			

	def __init__(self):
		self.position = None

		#sub2 = rospy.Subscriber('/chatter', String, self.callback2)
		sub = rospy.Subscriber('/wrench', WrenchStamped, self.callback)		
		rospy.spin()

			
	#def callback2(self, data2):
		
		#print ("mode "+str(data2.data))
	
	def callback(self, data):
		self.position = data.wrench.force.z
		print(self.position)
		#print(self.callback2)	

		try:
			connection.send('|'.encode()+str(self.position).encode())

		except:
			connection.close()
			#rospy.shutdown()
			print("closing connection")
			exit()
			
		
		
	

	

if __name__ == '__main__':
    rospy.init_node('wearami_socket', anonymous=True)
    #print("mode 0")
    s = Nodo()	
    
connection.close()

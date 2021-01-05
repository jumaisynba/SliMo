#!/usr/bin/env python
import socket
import sys
import rospy
import json
from geometry_msgs.msg import Wrench, WrenchStamped
from std_msgs.msg import Float64

# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the port
server_address = ("192.168.2.114", 8052)
print (sys.stderr, "starting up on %s port %s" % server_address)


sock.bind(("192.168.2.150", 8052))

# Listen for incoming connections
sock.listen(1)

print (sys.stderr, "waiting for a connection")
connection, client_address = sock.accept()
print (sys.stderr, "connection from", client_address)


class Nodo():
    def __init__(self):
        self.position = None
        sub = rospy.Subscriber('/wrench',
                               WrenchStamped, self.callback)

        print ("I was here")
        print (sub)
        rospy.spin()

    def callback(self, data):
        self.position = data.wrench.force.z
        print (self.position)
        try:
            connection.send('|'+str(self.position))
        except:
            connection.close()
            print("closing connection")
        # ------------try sending data
        #connection.send("I have position")
        # finally:
        # Clean up the connection
        #

        # rospy.spin()

if __name__ == '__main__':
    rospy.init_node('wearami_socket', anonymous=True)
    print ("also here")
    Nodo()

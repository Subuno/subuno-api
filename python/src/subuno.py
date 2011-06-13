#!/usr/bin/env python
#
# Copyright 2011 MERS Technologies.
#

"""Python 2.6/2.7 client library for the Subuno API.

This client library is designed to support the Subuno API. Read more
about the SUBUNO API at subuno.com. You can download this API at
http://github.com/subuno/api/

"""

import urllib, urllib2
import json

SUBUNO_SERVER_URI = "http://api.subuno.com/v1/"

class SUBUNOAPI(object):
	"""A client for the SUBUNO API.
	
	See subuno.com for complete API documentation.
	"""

	_apikey = None
	_server_uri = None
	
	def run(self, apikey, data, server_uri = SUBUNO_SERVER_URI):
		self._set_authentication_info(apikey=apikey, server_uri=server_uri)
		return self._call_server(data)
	
	def _set_authentication_info(self, apikey, server_uri = SUBUNO_SERVER_URI):
		self._apikey = apikey
		self._server_uri = server_uri
		return
	
	def _call_server(self, args):
		if self._apikey:

			#create data packet.
			data = {}
			for i in args:
				data[i] = args[i]
			
			#add apikey to the data packet.
			data["apikey"] = self._apikey
			
			#serialize data.
			urlencoding = urllib.urlencode(data)
			
			url = "%s?%s" % (self._server_uri, urlencoding)
			
			#perform request
			print url
			request = urllib2.Request(url = url)

			try:
				result = urllib2.urlopen(request).read()
			except urllib2.URLError:
				import sys
				raise SUBUNOAPIError("Server returned error or access denied: " + str(sys.exc_info()[1]))
			except:
				raise
			
			try:
				return json.loads(result)
			except ValueError:
				raise SUBUNOAPIError("Value doesn't convert to json object: '"+str(result)+"'")
			except:
				raise
				
		else:
			raise SUBUNOAPIError("API key not set.")

		
class SUBUNOAPIError(Exception):
    def __init__(self, message):
        Exception.__init__(self, message)

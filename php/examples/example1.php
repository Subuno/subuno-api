#!/usr/bin/env php
<?php
#
# Copyright 2011 MERS Technologies.
#

include "subuno.php";
$api = new SUBUNOAPI();
$result = $api->run(
	#apikey
		"2g4g747g843",
	#data
		array(
			t_id            => "7d3n89wn" ,
			ip_addr         => "24.24.24.24",
			customer_name   => "John Doe",
			phone           => "212-456-7890",
			email           => "john.doe@domain.com",
			company         => "Doe LLC",
			price           => "50.0",
			bin             => "480128",

			bill_street1    => "12 East 71th St",
			bill_street2    => "#12",
			bill_city       => "New York",
			bill_state      => "NY",
			bill_country    => "US" ,
			bill_zip        => "10021",

			ship_street1    => "12 East 71th St",
			ship_street2    => "#12",
			ship_city       => "New York",
			ship_state      => "NY",
			ship_country    => "US",
			ship_zip        => "10021",
			
			avs_response    => "X",
			ccv_response    => "M",
			custom1         => "first custom value",
			custom2         => "second custom value",
			custom3         => "third custom value",
			issuer_phone    => "18667750556",
			source          => "affiliate_code_here"
		)
);

#result is a php array with keys/value pairs with data returned by api.
print_r ($result);

?>

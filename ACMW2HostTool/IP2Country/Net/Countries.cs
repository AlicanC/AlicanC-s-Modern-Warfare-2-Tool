/*
 * GameWatch - Server Browser for online games
 * Copyright (C) 2003 Rodrigo Reyes <reyes@charabia.net>
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
 * 02111-1307, USA.
 *
 */
using System;
using System.Collections;

namespace GameWatch.Utils.Net
{
    public class Countries
    {
	private static Countries s_countries = new Countries();
	
	public ArrayList ContinentList = new ArrayList();

	public Hashtable ISOToCountryName = new Hashtable();
	public Hashtable CountryNameToISO = new Hashtable();

	public Hashtable Continents = new Hashtable();

	static public Countries Instance
	{
	    get { return s_countries; }
	}

	private Countries()
	{
	    AddCountry("Africa", "Central", "Burundi", "BI");
	    AddCountry("Africa", "Central", "Central African Republic", "CF");
	    AddCountry("Africa", "Central", "Chad", "TD");
	    AddCountry("Africa", "Central", "Congo", "CG");
	    AddCountry("Africa", "Central", "Rwanda", "RW");
	    AddCountry("Africa", "Central", "Zaire (Congo)", "ZR");

	    AddCountry("Africa", "Eastern", "Djibouti", "DJ");
	    AddCountry("Africa", "Eastern", "Eritrea", "ER");
	    AddCountry("Africa", "Eastern", "Ethiopia", "ET");
	    AddCountry("Africa", "Eastern", "Kenya", "KE");
	    AddCountry("Africa", "Eastern", "Somalia", "SO");
	    AddCountry("Africa", "Eastern", "Tanzania", "TZ");
	    AddCountry("Africa", "Eastern", "Uganda", "UG");

	    AddCountry("Africa", "Other", "Comoros", "KM");
	    AddCountry("Africa", "Other", "Madagascar", "MG");
	    AddCountry("Africa", "Other", "Mauritius", "MU");
	    AddCountry("Africa", "Other", "Mayotte", "YT");
	    AddCountry("Africa", "Other", "Reunion", "RE");
	    AddCountry("Africa", "Other", "Seychelles", "SC");

	    AddCountry("Africa", "Northern", "Algeria", "DZ");
	    AddCountry("Africa", "Northern", "Egypt", "EG");
	    AddCountry("Africa", "Northern", "Libya", "LY");
	    AddCountry("Africa", "Northern", "Morocco", "MA");
	    AddCountry("Africa", "Northern", "Sudan", "SD");
	    AddCountry("Africa", "Northern", "Tunisia", "TN");
	    AddCountry("Africa", "Northern", "Western Sahara", "EH");

	    AddCountry("Africa", "Southern", "Angola", "AO");
	    AddCountry("Africa", "Southern", "Botswana", "BW");
	    AddCountry("Africa", "Southern", "Lesotho", "LS");
	    AddCountry("Africa", "Southern", "Malawi", "MW");
	    AddCountry("Africa", "Southern", "Mozambique", "MZ");
	    AddCountry("Africa", "Southern", "Namibia", "NA");
	    AddCountry("Africa", "Southern", "South Africa", "ZA");
	    AddCountry("Africa", "Southern", "Swaziland", "SZ");
	    AddCountry("Africa", "Southern", "Zambia", "ZM");
	    AddCountry("Africa", "Southern", "Zimbabwe", "ZW");

	    AddCountry("Africa", "Western", "Benin", "BJ");
	    AddCountry("Africa", "Western", "Burkina Faso", "BF");
	    AddCountry("Africa", "Western", "Cameroon", "CM");
	    AddCountry("Africa", "Western", "Cape Verde", "CV");
	    AddCountry("Africa", "Western", "Cote d'Ivoire", "CI");
	    AddCountry("Africa", "Western", "Equatorial Guinea", "GQ");
	    AddCountry("Africa", "Western", "Gabon", "GA");
	    AddCountry("Africa", "Western", "Gambia, The", "GM");
	    AddCountry("Africa", "Western", "Ghana", "GH");
	    AddCountry("Africa", "Western", "Guinea", "GN");
	    AddCountry("Africa", "Western", "Guinea-Bissau", "GW");
	    AddCountry("Africa", "Western", "Liberia", "LR");
	    AddCountry("Africa", "Western", "Mali", "ML");
	    AddCountry("Africa", "Western", "Mauritania", "MR");
	    AddCountry("Africa", "Western", "Niger", "NE");
	    AddCountry("Africa", "Western", "Nigeria", "NG");
	    AddCountry("Africa", "Western", "Sao Tome and Principe", "ST");
	    AddCountry("Africa", "Western", "Senegal", "SN");
	    AddCountry("Africa", "Western", "Sierra Leone", "SL");
	    AddCountry("Africa", "Western", "Togo", "TG");

	    AddCountry("America", "Central", "Belize", "BZ");
	    AddCountry("America", "Central", "Costa Rica", "CR");
	    AddCountry("America", "Central", "El Salvador", "SV");
	    AddCountry("America", "Central", "Guatemala", "GT");
	    AddCountry("America", "Central", "Honduras", "HN");
	    AddCountry("America", "Central", "Mexico", "MX");
	    AddCountry("America", "Central", "Nicaragua", "NI");
	    AddCountry("America", "Central", "Panama", "PA");
	    AddCountry("America", "North", "Canada", "CA");
	    AddCountry("America", "North", "Greenland", "GL");
	    AddCountry("America", "North", "Saint-Pierre et Miquelon", "PM");
	    AddCountry("America", "North", "United States", "US");
	    AddCountry("America", "South", "Argentina", "AR");
	    AddCountry("America", "South", "Bolivia", "BO");
	    AddCountry("America", "South", "Brazil", "BR");
	    AddCountry("America", "South", "Chile", "CL");
	    AddCountry("America", "South", "Colombia", "CO");
	    AddCountry("America", "South", "Ecuador", "EC");
	    AddCountry("America", "South", "Falkland Islands", "FK");
	    AddCountry("America", "South", "French Guiana", "GF");
	    AddCountry("America", "South", "Guyana", "GY");
	    AddCountry("America", "South", "Paraguay", "PY");
	    AddCountry("America", "South", "Peru", "PE");
	    AddCountry("America", "South", "Suriname", "SR");
	    AddCountry("America", "South", "Uruguay", "UY");
	    AddCountry("America", "South", "Venezuela", "VE");

	    AddCountry("America", "West Indies", "Anguilla", "AI");
	    AddCountry("America", "West Indies", "Antigua&Barbuda", "AG");
	    AddCountry("America", "West Indies", "Aruba", "AW");
	    AddCountry("America", "West Indies", "Bahamas, The", "BS");
	    AddCountry("America", "West Indies", "Barbados", "BB");
	    AddCountry("America", "West Indies", "Bermuda", "BM");
	    AddCountry("America", "West Indies", "British Virgin Islands", "VG");
	    AddCountry("America", "West Indies", "Cayman Islands", "KY");
	    AddCountry("America", "West Indies", "Cuba", "CU");
	    AddCountry("America", "West Indies", "Dominica", "DM");
	    AddCountry("America", "West Indies", "Dominican Republic", "DO");
	    AddCountry("America", "West Indies", "Grenada", "GD");
	    AddCountry("America", "West Indies", "Guadeloupe", "GP");
	    AddCountry("America", "West Indies", "Haiti", "HT");
	    AddCountry("America", "West Indies", "Jamaica", "JM");
	    AddCountry("America", "West Indies", "Martinique", "MQ");
	    AddCountry("America", "West Indies", "Montserrat", "MS");
	    AddCountry("America", "West Indies", "Netherlands Antilles", "AN");
	    AddCountry("America", "West Indies", "Puerto Rico", "PR");
	    AddCountry("America", "West Indies", "Saint Kitts and Nevis", "KN");
	    AddCountry("America", "West Indies", "Saint Lucia", "LC");
	    AddCountry("America", "West Indies", "Saint Vincent and the Grenadines", "VC");
	    AddCountry("America", "West Indies", "Trinidad and Tobago", "TT");
	    AddCountry("America", "West Indies", "Turks and Caicos Islands", "TC");
	    AddCountry("America", "West Indies", "Virgin Islands", "VI");

	    AddCountry("Asia", "Central", "Kazakhstan", "KZ");
	    AddCountry("Asia", "Central", "Kyrgyzstan", "KG");
	    AddCountry("Asia", "Central", "Tajikistan", "TJ");
	    AddCountry("Asia", "Central", "Turkmenistan", "TM");
	    AddCountry("Asia", "Central", "Uzbekistan", "UZ");
	    AddCountry("Asia", "East", "China", "CN");
	    AddCountry("Asia", "East", "Japan", "JP");
	    AddCountry("Asia", "East", "Korea, North", "KP");
	    AddCountry("Asia", "East", "Korea, South", "KR");
	    AddCountry("Asia", "East", "Taiwan", "TW");
	    AddCountry("Asia", "Northern", "Mongolia", "MN");
	    AddCountry("Asia", "Northern", "Russia", "RU");
	    AddCountry("Asia", "South", "Afghanistan", "AF");
	    AddCountry("Asia", "South", "Bangladesh", "BD");
	    AddCountry("Asia", "South", "Bhutan", "BT");
	    AddCountry("Asia", "South", "India", "IN");
	    AddCountry("Asia", "South", "Maldives", "MV");
	    AddCountry("Asia", "South", "Nepal", "NP");
	    AddCountry("Asia", "South", "Pakistan", "PK");
	    AddCountry("Asia", "South", "Sri Lanka", "LK");
	    AddCountry("Asia", "South East", "Brunei", "BN");
	    AddCountry("Asia", "South East", "Cambodia", "KH");
	    AddCountry("Asia", "South East", "Christmas Island", "CX");
	    AddCountry("Asia", "South East", "Cocos (Keeling) Islands", "CC");
	    AddCountry("Asia", "South East", "Indonesia", "ID");
	    AddCountry("Asia", "South East", "Laos", "LA");
	    AddCountry("Asia", "South East", "Malaysia", "MY");
	    AddCountry("Asia", "South East", "Myanmar (Burma)", "MM");
	    AddCountry("Asia", "South East", "Philippines", "PH");
	    AddCountry("Asia", "South East", "Singapore", "SG");
	    AddCountry("Asia", "South East", "Thailand", "TH");
	    AddCountry("Asia", "South East", "Vietnam", "VN");
	    AddCountry("Asia", "South West", "Armenia", "AM");
	    AddCountry("Asia", "South West", "Azerbaijan", "AZ");
	    AddCountry("Asia", "South West", "Bahrain", "BH");
	    AddCountry("Asia", "South West", "Cyprus", "CY");
	    AddCountry("Asia", "South West", "Georgia", "GE");
	    AddCountry("Asia", "South West", "Iran", "IR");
	    AddCountry("Asia", "South West", "Iraq", "IQ");
	    AddCountry("Asia", "South West", "Israel", "IL");
	    AddCountry("Asia", "South West", "Jordan", "JO");
	    AddCountry("Asia", "South West", "Kuwait", "KW");
	    AddCountry("Asia", "South West", "Lebanon", "LB");
	    AddCountry("Asia", "South West", "Oman", "OM");
	    AddCountry("Asia", "South West", "Qatar", "QA");
	    AddCountry("Asia", "South West", "Saudi Arabia", "SA");
	    AddCountry("Asia", "South West", "Syria", "SY");
	    AddCountry("Asia", "South West", "Turkey", "TR");
	    AddCountry("Asia", "South West", "United Arab Emirates", "AE");
	    AddCountry("Asia", "South West", "Yemen", "YE");


	    AddCountry("Europe", "Central", "Austria", "AT");
	    AddCountry("Europe", "Central", "Czech Republic", "CZ");
	    AddCountry("Europe", "Central", "Hungary", "HU");
	    AddCountry("Europe", "Central", "Liechtenstein", "LI");
	    AddCountry("Europe", "Central", "Slovakia", "SK");
	    AddCountry("Europe", "Central", "Switzerland", "CH");
	    AddCountry("Europe", "Eastern", "Belarus", "BY");
	    AddCountry("Europe", "Eastern", "Estonia", "EE");
	    AddCountry("Europe", "Eastern", "Latvia", "LV");
	    AddCountry("Europe", "Eastern", "Lithuania", "LT");
	    AddCountry("Europe", "Eastern", "Moldova", "MD");
	    AddCountry("Europe", "Eastern", "Poland", "PL");
	    AddCountry("Europe", "Eastern", "Ukraine", "UA");
	    AddCountry("Europe", "Northern", "Denmark", "DK");
	    AddCountry("Europe", "Northern", "Faroe Islands", "FO");
	    AddCountry("Europe", "Northern", "Finland", "FI");
	    AddCountry("Europe", "Northern", "Iceland", "IS");
	    AddCountry("Europe", "Northern", "Norway", "NO");
	    AddCountry("Europe", "Northern", "Svalbard", "SJ");
	    AddCountry("Europe", "Northern", "Sweden", "SE");
	    AddCountry("Europe", "South East", "Albania", "AL");
	    AddCountry("Europe", "South East", "Bosnia Herzegovina", "BA");
	    AddCountry("Europe", "South East", "Bulgaria", "BG");
	    AddCountry("Europe", "South East", "Croatia", "HR");
	    AddCountry("Europe", "South East", "Greece", "GR");
	    AddCountry("Europe", "South East", "Macedonia", "MK");
	    AddCountry("Europe", "South East", "Romania", "RO");
	    AddCountry("Europe", "South East", "Slovenia", "SI");
	    AddCountry("Europe", "South West", "Andorra", "AD");
	    AddCountry("Europe", "South West", "Gibraltar", "GI");
	    AddCountry("Europe", "South West", "Portugal", "PT");
	    AddCountry("Europe", "South West", "Spain", "ES");
	    AddCountry("Europe", "Southern", "Vatican", "VA");
	    AddCountry("Europe", "Southern", "Italy", "IT");
	    AddCountry("Europe", "Southern", "Malta", "MT");
	    AddCountry("Europe", "Southern", "San Marino", "SM");
	    AddCountry("Europe", "Western", "Belgium", "BE");
	    AddCountry("Europe", "Western", "France", "FR");
	    AddCountry("Europe", "Western", "Germany", "DE");
	    AddCountry("Europe", "Western", "Ireland", "IE");
	    AddCountry("Europe", "Western", "Luxembourg", "LU");
	    AddCountry("Europe", "Western", "Monaco", "MC");
	    AddCountry("Europe", "Western", "Netherlands", "NL");
	    AddCountry("Europe", "Western", "United Kingdom", "GB");

	    AddCountry("Oceania", "Pacific", "American Samoa", "AS");
	    AddCountry("Oceania", "Pacific", "Australia", "AU");
	    AddCountry("Oceania", "Pacific", "Cook Islands", "CK");
	    AddCountry("Oceania", "Pacific", "Fiji", "FJ");
	    AddCountry("Oceania", "Pacific", "French Polynesia", "PF");
	    AddCountry("Oceania", "Pacific", "Guam", "GU");
	    AddCountry("Oceania", "Pacific", "Kiribati", "KI");
	    AddCountry("Oceania", "Pacific", "Marshall Islands", "MH");
	    AddCountry("Oceania", "Pacific", "Micronesia", "FM");
	    AddCountry("Oceania", "Pacific", "Nauru", "NR");
	    AddCountry("Oceania", "Pacific", "New Caledonia", "NC");
	    AddCountry("Oceania", "Pacific", "New Zealand", "NZ");
	    AddCountry("Oceania", "Pacific", "Niue", "NU");
	    AddCountry("Oceania", "Pacific", "Norfolk Island", "NF");
	    AddCountry("Oceania", "Pacific", "Northern Mariana Islands", "MP");
	    AddCountry("Oceania", "Pacific", "Palau", "PW");
	    AddCountry("Oceania", "Pacific", "Papua New-Guinea", "PG");
	    AddCountry("Oceania", "Pacific", "Pitcairn Islands", "PN");
	    AddCountry("Oceania", "Pacific", "Solomon Islands", "SB");
	    AddCountry("Oceania", "Pacific", "Tokelau", "TK");
	    AddCountry("Oceania", "Pacific", "Tonga", "TO");
	    AddCountry("Oceania", "Pacific", "Tuvalu", "TV");
	    AddCountry("Oceania", "Pacific", "Vanuatu", "VU");
	    AddCountry("Oceania", "Pacific", "Wallis & Futuna", "WF");
	    AddCountry("Oceania", "Pacific", "Western Samoa", "WS");
	}

	public void AddCountry(string continent, string region, string countryname, string iso)
	{
	    this.ISOToCountryName[String.Intern(iso)] = String.Intern(countryname);
	    this.CountryNameToISO[String.Intern(countryname)] = String.Intern(iso);

	    Continent c = (Continent) this.Continents[continent];
	    if (c == null)
		{
		    c = new Continent();
		    this.Continents[continent] = c;
		}

	    Region r = (Region)c.Regions[region];
	    if (r == null)
		{
		    r = new Region();
		    c.Regions[region] = r;
		}
	    r.CountryNames[countryname] = iso;
	}

	public class Continent
	{
	    public Hashtable Regions = new Hashtable();
	}

	public class Region
	{
	    public Hashtable CountryNames = new Hashtable();
	}

    }


}

// DEFINE VARIABLES - ONLY CHANGE visitor_name and protagonist_name
VAR visitor_name = ""
VAR protagonist_name = ""
VAR dialogue_state = ""
VAR talking = ""

// DIALOGUE STATE = START
-> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
Evening, sir.
* [>] -> section_2
* [Skip] -> section_12

== section_2 ==
~ talking = protagonist_name
Good evening, welcome to Nocturne Hotel. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Ah, yes, here it is.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Dupont. It’s a pleasure to have you. How was your trip to the hotel?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
I must admit, the streets here are rather lively at night.
* [>] -> section_6

== section_6 ==
~ talking = protagonist_name
They sure are. Some guests enjoy taking late-night strolls. Is that something you like to do?
* [>] -> section_7

== section_7 ==
~ talking = visitor_name
Perhaps ... Actually, I was wondering if there’s a shop nearby where I could buy some cigarettes.
* [>] -> section_8

== section_8 ==
~ talking = protagonist_name
Of course. There’s a convenience store two blocks away from here.
* [>] -> section_9

== section_9 ==
While we’re at it, could you please fill out this document for your ID and room requirements confirmation?
* [>] -> section_10

== section_10 ==
~ talking = visitor_name
Ah, yes, give me a moment ...
* [>] -> section_11

== section_11 ==
~ dialogue_state = "get_document"
~ talking = visitor_name
Here you go.
* [>] -> section_11a

== section_11a ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Thank you, Mr. Dupont.
* [>] -> section_12

== section_12 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Let me prepare the room for you.
* [x] -> section_13
* [x] -> section_13

== section_13 ==
~ dialogue_state = "card_given"
~ talking = protagonist_name
Your room is ready sir, and set as per your request.
* [>] -> section_14

== section_14 ==
Here’s your room access card; you’ll be on the fourth floor, room 412. Enjoy your stay.
* [>] -> section_15

== section_15 ==
~ talking = visitor_name
Merci. I’ll settle in and might explore a bit later. Have a good evening.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END

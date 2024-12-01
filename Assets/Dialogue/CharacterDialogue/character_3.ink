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
Uhm, hello.
* [>] -> section_2
* [Skip] -> section_14

== section_2 ==
~ talking = protagonist_name
Hello sir, welcome to Nocturne Hotel. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Evening. Sure, here you go.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Hythorne.
* [>] -> section_5

== section_5 ==
Just a heads-up, we have a no-smoking policy in the lobby area. I’d kindly ask you to extinguish your cigarette, please.
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
Ah, apologies. Old habits die hard, you know.
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
Very understandable sir.
* [>] -> section_8

== section_8 ==
Let me help you settle in. Could you please fill out this document for room requirements confirmation?
* [>] -> section_9

== section_9 ==
~ talking = visitor_name
~ dialogue_state = "get_document"
Sure thing. Here you go.
* [>] -> section_10

== section_10 ==
~ talking = protagonist_name
~ dialogue_state = "finish_document"
Thank you, Mr. Hythorne. I see you’ve requested 11-day stay—quite the commitment.
* [>] -> section_11

== section_11 ==
Planning anything special during your visit?
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
Just... some personal work.
* [>] -> section_13

== section_13 ==
The kind that needs safe, peace and quiet, you know?
* [>] -> section_14

== section_14 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Of course, we’ll ensure your stay is as comfortable as possible. Wait a moment please.
* [x] -> section_15
* [x] -> section_16

== section_15 ==
~ dialogue_state = "card_given"
Your room is ready, and everything has been prepared. Here’s your room access card; you’ll be on the third floor, room 317.
* [>] -> section_17

== section_16 ==
~ dialogue_state = "card_given"
Your room is ready, and everything has been prepared. Here’s your room access card; you’ll be on the third floor, room 317.
* [>] -> section_18

== section_17 ==
~ talking = visitor_name
Perfect. Thanks for your help, mate.
* [>] -> section_end

== section_18 ==
~ talking = visitor_name
Ehm, are you sure everything has been prepared?
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END

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
Good eveniiing. I have… a ressservation. Eryn Castor.
* [>] -> section_2
* [Skip] -> section_13

== section_2 ==
~ talking = protagonist_name
Good evening, Mr. Castor. Welcome to Nocturne. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Of courssse. I trussst everything… issss in order.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Castor. Let me check… Ah, I see your booking here.
* [>] -> section_5

== section_5 ==
Could you please fill out this document to confirm your special requests for your stay?
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
Ssssspecial requessstsss… yesss, I have them.
* [>] -> section_7

== section_7 ==
~ dialogue_state = "get_document"
Sssusssshi… with extra wasssabi… and sssake. You agree, don’t you? Perfect for a refined… palate.
* [>] -> section_8

== section_8 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Excellent choices, Mr. Castor. I’ll make sure everything is prepared exactly as you requested.
* [>] -> section_9

== section_9 ==
~ talking = visitor_name
Actually… I wasss alssso wondering… if you could recommend the bessst way to get to the zzzoo nearby.
* [>] -> section_10

== section_10 ==
I’ve alwaaaysss wanted to ssssee a lion… and maleo birdss… up clossse.
* [>] -> section_11

== section_11 ==
~ talking = protagonist_name
I’d suggest taking the tram—it’s direct and quite scenic.
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
Tram… yesss, efficient and… pleasssant. You are… quite resssourceful.
* [>] -> section_13

== section_13 ==
~ dialogue_state = "card"
~ talking = protagonist_name
We do our best, sir. —and wait a moment please.
* [x] -> section_14
* [x] -> section_14a

== section_14 ==
~ dialogue_state = "card_given"
Your room is ready—room 409 on the fourth floor. Here’s your access card.
* [>] -> section_15

== section_14a ==
~ dialogue_state = "card_given"
Your room is ready—room 409 on the fourth floor. Here’s your access card.
* [>] -> section_16

== section_15 ==
~ talking = visitor_name
Thaaank you. Everything… better be flawwwlessss.
* [>] -> section_end

== section_16 ==
~ talking = visitor_name
I think ... I ssshould get out of here ... and go to the zzzzoo ... very soon .. with brother.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END

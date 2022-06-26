# Tips
    - Use C# events most of the time, 'cause:
        1. Compile time safety. UnityEvents do let you specify types too, however they are very limited in what they can do. If your C# event is given the wrong object, it won't compile. A UnityEvent, on the other hand, can call just about any function on any object. This may be an advantage for the use case above where the object is supposed to interact with virtually anything, but is a serious disadvantage otherwise.
        2. Less bloat in your inspector, especially if something is going to be done in all cases regardless of specific instances
        3. Business logic is well defined in code, rather than being largely arbitrary as a series of disconnected Unity objects that call each others functions through events. Instead, classes/interfaces can be paired together with the caller/listener which makes things easier to organise and understand, with less potential for wacky behaviour because a designer accidentally added Player.Kill to an event in the skybox
    - Use Unity events when You need to create tools for Your designers or for very small stuff, like in the last video

    - Make Subscriptions in **Awake** method and everything else - in **Start**. This way You can avoid problem, when event has been called, but without subscribers
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Reminder - version with EventHandler, if needed one day
 * protected event EventHandler<MessageEventArgs> OnArriveToStation;


public void RegisterOnArriveToStation(Action<object,MessageEventArgs> a_action)
{
    EventHandler<MessageEventArgs> evenHandler = (a_object, a_message) => a_action(a_object, a_message);  
    OnArriveToStation += evenHandler;
}*/


//------------ LIST OF EVENTS -------------//
//NameOfEvent;Param1;Param2

//##BEGIN##

//Start
//RageIncrease;NumberEventArgs
//RageUpdate;NumberEventArgs
//ScoreUpdate;NumberEventArgs
//GiveItem;ItemEventArgs
//GiveRandomItem
//ClientComplete;ClientEventArgs
//Loose
//##END##

//------------ END - LIST OF EVENTS -------------//


public class EventManager : Singleton<EventManager>
{

// ----- AUTO GENERATED CODE ----- //




// --- EVENT --- Start --- //


	protected event Action<object> OnStart;
	public void RegisterOnStart(Action<object> a_action)
	{
		OnStart += a_action;
	}


	public void UnRegisterOnStart(Action<object> a_action)
	{
		OnStart -= a_action;
	}


	public void InvokeOnStart(object a_sender)
	{
		if(OnStart != null)
		{
			OnStart.Invoke(a_sender);
		}
	}




// --- EVENT --- RageIncrease --- //


	protected event Action<object, NumberEventArgs> OnRageIncrease;
	public void RegisterOnRageIncrease(Action<object, NumberEventArgs> a_action)
	{
		OnRageIncrease += a_action;
	}


	public void UnRegisterOnRageIncrease(Action<object, NumberEventArgs> a_action)
	{
		OnRageIncrease -= a_action;
	}


	public void InvokeOnRageIncrease(object a_sender, NumberEventArgs a_numberEventArgs)
	{
		if(OnRageIncrease != null)
		{
			OnRageIncrease.Invoke(a_sender, a_numberEventArgs);
		}
	}




// --- EVENT --- RageUpdate --- //


	protected event Action<object, NumberEventArgs> OnRageUpdate;
	public void RegisterOnRageUpdate(Action<object, NumberEventArgs> a_action)
	{
		OnRageUpdate += a_action;
	}


	public void UnRegisterOnRageUpdate(Action<object, NumberEventArgs> a_action)
	{
		OnRageUpdate -= a_action;
	}


	public void InvokeOnRageUpdate(object a_sender, NumberEventArgs a_numberEventArgs)
	{
		if(OnRageUpdate != null)
		{
			OnRageUpdate.Invoke(a_sender, a_numberEventArgs);
		}
	}




// --- EVENT --- ScoreUpdate --- //


	protected event Action<object, NumberEventArgs> OnScoreUpdate;
	public void RegisterOnScoreUpdate(Action<object, NumberEventArgs> a_action)
	{
		OnScoreUpdate += a_action;
	}


	public void UnRegisterOnScoreUpdate(Action<object, NumberEventArgs> a_action)
	{
		OnScoreUpdate -= a_action;
	}


	public void InvokeOnScoreUpdate(object a_sender, NumberEventArgs a_numberEventArgs)
	{
		if(OnScoreUpdate != null)
		{
			OnScoreUpdate.Invoke(a_sender, a_numberEventArgs);
		}
	}




// --- EVENT --- GiveItem --- //


	protected event Action<object, ItemEventArgs> OnGiveItem;
	public void RegisterOnGiveItem(Action<object, ItemEventArgs> a_action)
	{
		OnGiveItem += a_action;
	}


	public void UnRegisterOnGiveItem(Action<object, ItemEventArgs> a_action)
	{
		OnGiveItem -= a_action;
	}


	public void InvokeOnGiveItem(object a_sender, ItemEventArgs a_itemEventArgs)
	{
		if(OnGiveItem != null)
		{
			OnGiveItem.Invoke(a_sender, a_itemEventArgs);
		}
	}




// --- EVENT --- GiveRandomItem --- //


	protected event Action<object> OnGiveRandomItem;
	public void RegisterOnGiveRandomItem(Action<object> a_action)
	{
		OnGiveRandomItem += a_action;
	}


	public void UnRegisterOnGiveRandomItem(Action<object> a_action)
	{
		OnGiveRandomItem -= a_action;
	}


	public void InvokeOnGiveRandomItem(object a_sender)
	{
		if(OnGiveRandomItem != null)
		{
			OnGiveRandomItem.Invoke(a_sender);
		}
	}




// --- EVENT --- ClientComplete --- //


	protected event Action<object, ClientEventArgs> OnClientComplete;
	public void RegisterOnClientComplete(Action<object, ClientEventArgs> a_action)
	{
		OnClientComplete += a_action;
	}


	public void UnRegisterOnClientComplete(Action<object, ClientEventArgs> a_action)
	{
		OnClientComplete -= a_action;
	}


	public void InvokeOnClientComplete(object a_sender, ClientEventArgs a_clientEventArgs)
	{
		if(OnClientComplete != null)
		{
			OnClientComplete.Invoke(a_sender, a_clientEventArgs);
		}
	}




// --- EVENT --- Loose --- //


	protected event Action<object> OnLoose;
	public void RegisterOnLoose(Action<object> a_action)
	{
		OnLoose += a_action;
	}


	public void UnRegisterOnLoose(Action<object> a_action)
	{
		OnLoose -= a_action;
	}


	public void InvokeOnLoose(object a_sender)
	{
		if(OnLoose != null)
		{
			OnLoose.Invoke(a_sender);
		}
	}
// ----- END AUTO GENERATED CODE ----- //

}


public class MessageEventArgs : EventArgs
{
    public string m_message;
    public MessageEventArgs(string a_message)
    {
        m_message = a_message;
    }
}

public class NumberEventArgs : EventArgs
{
    public float m_number;
    public NumberEventArgs(float a_number)
    {
        m_number = a_number;
    }
}



public class ItemEventArgs : EventArgs
{
    public ThrowableItemType m_itemType;
    public ItemEventArgs(ThrowableItemType a_itemType)
    {
        m_itemType = a_itemType;
    }
}


public class ClientEventArgs : EventArgs
{
    public Client m_client;
    public ClientEventArgs(Client a_client)
    {
        m_client = a_client;
    }
}



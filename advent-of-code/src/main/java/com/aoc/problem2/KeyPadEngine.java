package com.aoc.problem2;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class KeyPadEngine {

	private HashMap<String, Key> keys;
	private Key position;
	private String name;
	
	private List<String> code;
	
	public KeyPadEngine(String name, HashMap<String, Key> keyEnv, Key start){
		//set up keypad
		this.keys = keyEnv;
		this.name = name;
		this.code = new ArrayList<String>();
		//set initial position
		this.position = start;
	}
	
	public void SubmitMove(Move m){
		String newKey = null;
		switch(m){
			case Up: newKey = position.getUp();
			break;
			case Down: newKey = position.getDown();
			break;
			case Left: newKey = position.getLeft();
			break;
			case Right: newKey = position.getRight();
			break;
		}
		if(newKey != null){
			position = keys.get(newKey);
		}
	}
	
	public String GetName(){
		return name;
	}
	
	public void LogKeyNumber(){
		code.add(position.getNumber());
	}
	
	public String GetCode(){
		StringBuilder sb = new StringBuilder();
		for(String i : code){
			sb.append(i);
		}
		return sb.toString();
	}
}

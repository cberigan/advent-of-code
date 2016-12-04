package com.aoc.problem4;

public class CharCountPair {
	Character chara;
	Integer count;
	
	public CharCountPair(Character c, Integer count){
		this.count = count;
		this.chara = c;
	}
	
	public Character getChar(){
		return this.chara;
	}
	
	public Integer getCount(){
		return this.count;
	}
	
	public void incCount(){
		this.count++;
	}
}

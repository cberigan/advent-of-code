package com.aoc.problem2;

public class Key {

	private Integer number;
	private Integer up;
	private Integer down;
	private Integer left;
	private Integer right;
	
	public Key(Integer num, Integer up, Integer down, Integer left, Integer right){
		this.number = num;
		this.up = up;
		this.down = down;
		this.left = left;
		this.right = right;
	}

	public Integer getNumber() {
		return number;
	}

	public Integer getUp() {
		return up;
	}
	
	public Integer getDown() {
		return down;
	}
	
	public Integer getRight() {
		return right;
	}
	
	public Integer getLeft() {
		return left;
	}
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((number == null) ? 0 : number.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Key other = (Key) obj;
		if (number == null) {
			if (other.number != null)
				return false;
		} else if (!number.equals(other.number))
			return false;
		return true;
	}
	
}

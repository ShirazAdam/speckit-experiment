import axios from 'axios';
import type { TodoItem } from '../types';

const api = axios.create({
  baseURL: 'https://localhost:5001/api/todos',
});

export const getTodos = () => api.get<TodoItem[]>('');
export const getTodo = (id: string) => api.get<TodoItem>(`/${id}`);
export const createTodo = (item: Partial<TodoItem>) => api.post<TodoItem>('', item);
export const updateTodo = (id: string, item: Partial<TodoItem>) => api.put(`/${id}`, item);
export const deleteTodo = (id: string) => api.delete(`/${id}`);

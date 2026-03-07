<template>
  <div>
    <div class="new-item">
      <input v-model="newTitle" @keyup.enter="add" placeholder="Add new to-do" />
      <button @click="add">Add</button>
    </div>
    <div v-if="todos.length === 0" class="empty">No items yet.</div>
    <TodoItem
      v-for="t in todos"
      :key="t.id"
      :item="t"
      @update="updateTodo"
      @delete="deleteTodo"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import type { TodoItem as TodoType } from '../types';
import {
  getTodos,
  createTodo,
  updateTodo as apiUpdate,
  deleteTodo as apiDelete,
} from '../services/todoService';
import TodoItem from './TodoItem.vue';

const todos = ref<TodoType[]>([]);
const newTitle = ref('');

async function load() {
  const response = await getTodos();
  todos.value = response.data;
}

async function add() {
  if (!newTitle.value.trim()) return;
  const response = await createTodo({ title: newTitle.value, isCompleted: false });
  todos.value.push(response.data);
  newTitle.value = '';
}

async function updateTodo(updated: TodoType) {
  await apiUpdate(updated.id, updated);
  const idx = todos.value.findIndex((t) => t.id === updated.id);
  if (idx !== -1) todos.value[idx] = updated;
}

async function deleteTodo(id: string) {
  await apiDelete(id);
  todos.value = todos.value.filter((t) => t.id !== id);
}

onMounted(load);
</script>

<style scoped>
.new-item {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
}
.new-item input {
  flex: 1;
  padding: 0.5rem;
}
.new-item button {
  padding: 0.5rem 1rem;
}
.empty {
  color: #666;
  font-style: italic;
}
</style>
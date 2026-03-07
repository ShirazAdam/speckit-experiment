<template>
  <div class="todo-item">
    <input type="checkbox" v-model="local.isCompleted" @change="toggle" />
    <span :class="{ completed: local.isCompleted }">{{ local.title }}</span>
    <button @click="remove">✕</button>
  </div>
</template>

<script setup lang="ts">
import type { TodoItem } from '../types';
import { reactive, watch } from 'vue';


interface Props {
  item: TodoItem;
}
interface Emits {
  (e: 'update', item: TodoItem): void;
  (e: 'delete', id: string): void;
}

const props = defineProps<Props>();
const emits = defineEmits<Emits>();

const local = reactive({ ...props.item });

watch(
  () => props.item,
  (newVal) => Object.assign(local, newVal),
);

function toggle() {
  emits('update', { ...local });
}

function remove() {
  emits('delete', local.id);
}
</script>

<style scoped>
.todo-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 0;
  border-bottom: 1px solid #eee;
}
.completed {
  text-decoration: line-through;
  color: #999;
}
button {
  background: none;
  border: none;
  cursor: pointer;
  color: #c00;
  font-size: 1rem;
}
</style>
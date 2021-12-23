import styled from 'styled-components'

export const Container = styled.div`
  width: 100%;
  height: 40px;

  display: flex;
  align-items: center;
  padding: 4px;

  border: solid 1px #ddd;
  border-radius: 4px;
`

export const InputElement = styled.input`
  width: 100%;
  padding: 0 10px;

  font-family: 'Fira Code', monospace;

  border: 0;
  outline-style: none;
  background: transparent;

  &::placeholder {
    text-decoration: wavy;
    font-style: italic;
    color: #aaa;
  }
`

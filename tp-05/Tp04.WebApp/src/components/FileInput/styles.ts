import styled from 'styled-components'

export const Container = styled.div`
  width: fit-content;

  display: flex;
  flex-direction: column;

  padding: 10px;
  border: solid 1px #ddd;

  border-radius: 4px;

  input {
    display: none;
  }

  > * {
    width: 100%;
  }
`

export const FileSelector = styled.label`
  display: flex;
  align-items: center;

  padding: 5px;

  border-radius: 4px;

  transition: background-color 0.25s ease;

  cursor: pointer;

  &:hover {
    background-color: #ddd;
  }

  strong {
    margin-left: 10px;
  }
`

export const InputDetails = styled.div`
  margin-top: 10px;

  width: 100%;
  display: flex;

  font-size: 14px;

  strong {
  }

  span {
    margin-left: 10px;
  }
`
